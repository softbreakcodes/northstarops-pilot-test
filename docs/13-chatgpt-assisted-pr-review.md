# 13 — ChatGPT Destekli Pull Request Review Modeli

## Amaç

NorthStarOps'ta tek human reviewer bulunduğunda her Pull Request'i baştan sona manuel incelemek ölçeklenebilir değildir. İlk aşamada üçüncü taraf AI review servisi zorunlu olmadan, ChatGPT'nin GitHub bağlantısı üzerinden bağlı Issue bağlamında ilk review'u yapması hedeflenir.

Bu modelde ChatGPT review operatörü olarak çalışır; final merge yetkisi maintainer'da kalır.

## Hedef akış

```text
Contributor
→ Issue /claim
→ Draft PR
→ Ready for Review
→ In Review
→ ChatGPT review
→ APPROVE veya REQUEST_CHANGES önerisi
→ gerekirse same branch / same PR revision
→ tekrar ChatGPT review
→ maintainer merge kararı
→ Squash Merge
→ Issue Closed
→ Done
```

## Review nasıl başlatılır?

Bu model background veya kendiliğinden çalışan bir reviewer değildir. PR `Ready for Review` olduğunda maintainer ChatGPT'ye PR numarasını veya GitHub linkini verir ve review ister.

Örnek operasyon talebi:

```text
northstarops #123 PR'ını bağlı Issue bağlamında review et.
```

ChatGPT GitHub bağlantısı üzerinden review için gerekli kaynakları okur.

## Review sırasında okunacak kaynaklar

Asgari bağlam:

- PR body ve bağlı `Closes #N` Issue,
- Issue `Business Problem`,
- `Expected Behaviour`,
- `Acceptance Criteria`,
- `Allowed Scope`,
- `Do Not Change`,
- PR diff'i ve changed files,
- commit'ler,
- mevcut review comment'leri,
- test/check sonuçları erişilebildiği ölçüde.

Review yalnızca "kod güzel mi?" sorusuna cevap vermemelidir. Temel soru şudur:

```text
Bu PR, claim edilen Issue'da istenen işi gerçekten ve yalnızca izin verilen kapsamda yapıyor mu?
```

## Standart review çıktısı

ChatGPT review sonucu en az şu başlıkları içermelidir:

```text
ISSUE ALIGNMENT: PASS / FAIL
SCOPE: PASS / FAIL
ACCEPTANCE CRITERIA:
- AC1: PASS / FAIL
- AC2: PASS / FAIL
TESTS: PASS / FAIL
CODE QUALITY: PASS / FAIL
RISK: LOW / MEDIUM / HIGH

RECOMMENDATION:
APPROVE / REQUEST_CHANGES
```

FAIL bulunan her maddede somut neden ve mümkünse ilgili dosya/satır belirtilmelidir.

## GitHub'a review bırakma

İki çalışma modu vardır.

### Mod A — Sadece rapor

ChatGPT review yapar ve sonucu maintainer'a verir. GitHub üzerinde review mutation yapılmaz.

### Mod B — GitHub review

Maintainer açıkça isterse ChatGPT GitHub bağlantısı üzerinden PR'a doğrudan:

- `APPROVE`,
- `REQUEST_CHANGES`,
- review comment

bırakabilir.

Bu mutation kullanıcı talebiyle yapılmalıdır. ChatGPT kendi başına merge yapmaz.

## Maintainer sorumluluğu

ChatGPT ilk reviewer iş yükünü azaltır ancak repository governance yetkisini devralmaz.

Maintainer:

- merge kararının sahibidir,
- ruleset ve branch protection'ın sahibidir,
- gerektiğinde AI review bulgularını yeniden kontrol eder,
- güvenlik, iş riski veya yüksek etkili değişikliklerde ek insan incelemesi isteyebilir.

## Revision akışı

`REQUEST_CHANGES` oluşursa contributor:

- yeni PR açmaz,
- aynı branch'e düzeltme commit'i gönderir,
- aynı PR üzerinde devam eder.

Düzeltme sonrasında ChatGPT'den aynı Issue bağlamında yeniden review istenir.

```text
REQUEST_CHANGES
→ same branch commit
→ same PR updated
→ re-review
→ APPROVE recommendation
```

## İlk aşama tercihi

NorthStarOps'un ilk aşamasında CodeRabbit veya başka bir üçüncü taraf AI reviewer zorunlu değildir.

Başlangıç modeli:

```text
GitHub governance workflows
+ ChatGPT GitHub review
+ human maintainer merge authority
```

Üçüncü taraf otomatik reviewer ancak PR hacmi veya operasyon ihtiyacı daha sonra bunu gerektirirse ayrı bir optimizasyon olarak değerlendirilebilir.

## Operasyon sınırı

ChatGPT review'un başlatılması için bir kullanıcı etkileşimi gerekir. Bu doküman sürekli background monitoring veya PR oluşur oluşmaz otomatik review garantisi vermez.

Eğer ileride tam otomatik tetikleme istenirse GitHub event'i ile çalışan ayrı bir automation/integration tasarlanmalıdır.

## PASS kriteri

Bu model başarılı kabul edilirken:

- bağlı Issue doğru okunur,
- scope ihlalleri yakalanır,
- Acceptance Criteria tek tek değerlendirilir,
- test eksikleri belirtilir,
- review sonucu `APPROVE` veya `REQUEST_CHANGES` önerisine indirgenir,
- revision aynı PR üzerinde yeniden review edilir,
- final merge yetkisi maintainer'da kalır.
