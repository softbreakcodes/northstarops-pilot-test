# 09 — Review, Revision ve Merge Akışı

## Amaç

Contributor'ın review feedback'i sonrası yeni PR açmadan aynı contribution üzerinde ilerlemesini sağlamak ve tek human reviewer'ın iş yükünü ChatGPT destekli ilk review ile azaltmak.

## Akış

```text
Draft PR
→ Ready for Review
→ In Review
→ ChatGPT-assisted Review
→ APPROVE veya REQUEST_CHANGES önerisi
→ gerekirse aynı branch'e yeni commit
→ aynı PR güncellenir
→ yeniden ChatGPT review
→ maintainer merge kararı
→ Squash Merge
→ Issue Closed
→ Done
```

ChatGPT review modelinin ayrıntıları `13-chatgpt-assisted-pr-review.md` içinde tanımlanmıştır.

## Kural: aynı branch / aynı PR

Review sonucu değişiklik istenirse contributor:

- yeni Issue açmaz,
- yeni PR açmaz,
- aynı branch üzerinde düzeltme yapar,
- commit + push ile mevcut PR'ı günceller.

Bu yaklaşım review context'ini, diff geçmişini ve Issue bağlantısını korur.

## ChatGPT-assisted review

İlk aşamada üçüncü taraf AI reviewer zorunlu değildir. PR `Ready for Review` olduktan sonra maintainer ChatGPT'ye PR numarası veya linki ile review talebi verir.

ChatGPT review sırasında bağlı Issue'yu, PR diff'ini, changed files'ı, test/check sonuçlarını ve mevcut review context'ini okuyarak en az şu boyutları değerlendirir:

```text
Issue Alignment
Scope
Acceptance Criteria
Tests
Code Quality
Risk
```

Sonuç `APPROVE` veya `REQUEST_CHANGES` önerisine indirgenir. Maintainer açıkça isterse ChatGPT GitHub'a doğrudan review bırakabilir; final merge yetkisi maintainer'da kalır.

## Branch adı

Branch isminde Issue numarası bulunması önerilir:

```text
feature/123-short-description
fix/123-short-description
```

## Merge

`main` yalnızca Pull Request üzerinden güncellenir. Production hedefinde:

- en az 1 approval,
- unresolved conversation olmaması,
- stale approval'ın yeni push sonrası geçersizleşmesi,
- yalnızca squash merge

kullanılması önerilir.

ChatGPT'nin `APPROVE` önerisi ile repository ruleset'in gerçek required approval kuralı aynı şey değildir. Production ruleset hangi GitHub review'larının merge requirement'a sayılacağını ayrıca belirlemelidir.

## Issue kapanışı

PR body:

```text
Closes #123
```

şeklinde olduğunda merge sonrası bağlı Issue GitHub tarafından otomatik kapanır.

## Project Done

Issue kapandıktan sonra Project kartının `Done` olması sağlanmalıdır. Bu işlem Project'in yerleşik automation'ı ile yapılabilir. Production Project kurulurken `Item closed → Status Done` automation'ı etkinleştirilmelidir.

## Pilot uçtan uca doğrulaması

Pilot lifecycle testinde:

- contributor claim etti,
- Draft PR açtı,
- Ready for Review oldu,
- maintainer Request Changes verdi,
- contributor aynı branch ve PR üzerinde revision yaptı,
- approval sonrası squash merge gerçekleşti,
- closing reference ile Issue kapandı.

ChatGPT-assisted review modeli bu mevcut lifecycle'ın üzerine reviewer iş yükünü azaltan operasyon katmanı olarak eklenmiştir; ayrı bir lifecycle yaratmaz.

## PASS kriteri

Review feedback sonrasında ikinci PR oluşmadan aynı PR ilerler; ChatGPT bağlı Issue bağlamında review üretir; final merge kararı maintainer tarafından verilir ve bağlı Issue merge sonrası kapanır.
