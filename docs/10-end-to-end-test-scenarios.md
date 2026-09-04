# 10 — Uçtan Uca Test Senaryoları

## Amaç

Production'a geçmeden önce governance altyapısının yalnızca dosya seviyesinde değil gerçek GitHub davranışı olarak çalıştığını doğrulamak.

## Test 1 — Başarılı claim

Başlangıç:

```text
Issue open
label = status: available
Project Status = Available
```

Eylem:

```text
contributor → /claim
```

Beklenen:

```text
assignee = contributor
label = status: claimed
Project Status = Claimed
24 saat Draft PR mesajı
```

## Test 2 — İkinci aktif claim reddi

Aynı contributor'ın bir aktif claim'i varken başka Available Issue'ya `/claim` yazması denenir.

Beklenen:

- ikinci Issue claim edilmez,
- assignee atanmaz,
- Available kalır,
- bot mevcut aktif claim'i bildirir.

## Test 3 — PR Admission PASS

Claim sahibi kendi fork/branch'inden Draft PR açar.

PR body:

```text
Closes #<claimed-issue>
```

Beklenen: PR açık kalır ve admission geçer.

## Test 4 — PR Admission FAIL

Aşağıdakiler ayrı ayrı denenebilir:

- closing reference yok,
- birden fazla Issue closing reference,
- Issue closed,
- Issue claimed değil,
- PR author ≠ Issue assignee.

Beklenen: PR bot açıklaması ile otomatik kapanır.

## Test 5 — Ready for Review

Draft PR Ready for Review yapılır.

Beklenen:

```text
Issue label = status: in-review
Project Status = In Review
```

## Test 6 — Convert to Draft

PR tekrar Draft'a çevrilir.

Beklenen:

```text
Issue label = status: claimed
Project Status = Claimed
```

## Test 7 — Review revision flow

PR tekrar Ready for Review yapılır. Maintainer Request Changes verir. Contributor aynı branch'e yeni commit push eder ve aynı PR üzerinden devam eder. Sonra approval verilir.

Beklenen: ikinci PR oluşmaz; review context korunur.

## Test 8 — Squash merge ve kapanış

Approved PR squash merge edilir.

Beklenen:

- PR merged,
- `Closes #N` nedeniyle Issue closed,
- Project item `Done`.

## Test 9 — Claim timeout

Yeni Issue claim edilir fakat PR açılmaz. `Claim Timeout` manuel olarak:

```text
timeout_hours = 0
```

çalıştırılır.

Beklenen:

```text
assignee kaldır
status: claimed kaldır
status: available ekle
Project Status = Available
```

## Test 10 — Project mutation failure / fail-closed

Mümkünse kontrollü test ortamında Project token veya Project Status configuration geçici olarak geçersiz hale getirilir.

Beklenen: claim veya timeout yarım state bırakmamalı; rollback çalışmalı ve workflow failure üretmelidir.

## Test 11 — ChatGPT-assisted Issue-context review

Bir test Issue açıkça ölçülebilir Acceptance Criteria, Allowed Scope ve Do Not Change kurallarıyla hazırlanır. Contributor PR içinde kontrollü bir contract veya scope ihlali yapar.

PR `Ready for Review` olduktan sonra maintainer ChatGPT'ye PR numarası/linki vererek bağlı Issue bağlamında review ister.

Beklenen:

- doğru bağlı Issue okunur,
- kontrollü ihlal yakalanır,
- Acceptance Criteria tek tek değerlendirilir,
- scope değerlendirilir,
- test eksikleri varsa belirtilir,
- sonuç `APPROVE` veya `REQUEST_CHANGES` önerisine indirgenir.

Contributor aynı branch/PR üzerinde düzeltme yaptıktan sonra re-review istenir.

Beklenen: eski bulgu giderilmiş olarak yeniden değerlendirilir ve yeni PR açılmaz.

## Release gate

Production contributor'ları davet etmeden önce Test 1–9 ve Test 11 PASS olmalıdır. Test 10 en az bir staging/pilot ortamında doğrulanmış olmalıdır.
