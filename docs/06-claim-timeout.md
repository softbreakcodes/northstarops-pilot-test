# 06 — 24 Saat Claim Timeout

## Amaç

Claim edilen fakat çalışmaya başlanmayan Issue'ların süresiz bloke olmasını engellemek.

## Workflow dosyası

```text
.github/workflows/claim-timeout.yml
```

## Çalışma biçimi

Workflow saatlik schedule ile çalışır ve ayrıca manuel test için `workflow_dispatch` destekler.

Production timeout:

```text
24 saat
```

Manuel testte `timeout_hours = 0` kullanılarak beklemeden doğrulama yapılabilir.

## Claim başlangıcını bulma

Timeout hesabı için bot'un claim confirmation yorumundaki gizli marker kullanılır:

```text
<!-- northstarops-claim -->
```

Bu yorumun `created_at` zamanı claim başlangıcıdır.

## Qualifying PR kontrolü

Deadline dolduğunda workflow Issue timeline'ındaki cross-reference event'lerden bağlı PR'ları bulur. Claim korunması için PR:

- claimant tarafından açılmış olmalı,
- claim deadline'ından önce oluşturulmuş olmalı,
- açık veya merge edilmiş olmalı.

## Timeout mutation'ları

Qualifying PR yoksa:

```text
assignee kaldır
status: claimed kaldır
status: available ekle
Project Status = Available
```

Bot timeout açıklaması bırakır.

## Rollback

Project Status `Available` yapılamazsa workflow Issue tarafındaki claim'i geri kurar. Böylece board ve Issue farklı state'lerde bırakılmaz.

## Pilot sırasında bulunan hata

Issue timeline sorgusunda `gh api --paginate ... -f per_page=100` kullanıldığında açıkça `-X GET` verilmediği için istek yanlış metoda döndü ve `404 / invalid control character in URL` hatası oluştu. Çözüm timeline çağrısında `-X GET` kullanmaktır.

## PASS testi

1. Test Issue Available.
2. Contributor `/claim` yazar.
3. Project `Claimed` olur.
4. PR açılmaz.
5. Claim Timeout manuel `timeout_hours = 0` çalıştırılır.
6. Assignee kaldırılır.
7. Label Available olur.
8. Project Status Available olur.
