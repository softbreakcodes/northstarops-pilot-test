---

name: NorthStarOps Work Item
about: NorthStarOps geliştirme görevleri için standart Issue şablonu
title: ""
labels: ""
assignees: ""
-------------

## Business Problem

Bu iş neden gerekli? Hangi gerçek problemi çözüyor?

## Expected Behaviour

İş tamamlandığında sistem nasıl davranmalı?

## Acceptance Criteria

* [ ] Beklenen davranış sağlanıyor.
* [ ] Hata / sınır durumları ele alındı.
* [ ] Mevcut davranışlar bozulmadı.

## Allowed Scope

Yalnızca aşağıdaki alanlarda değişiklik yapılabilir:

```text
src/...
tests/...
```

## Do Not Change

Aşağıdaki alanlara dokunulmamalıdır:

* unrelated modules
* database / migrations
* shared infrastructure
* CI/CD
* Issue kapsamı dışındaki kodlar

## Contribution Rules

* Issue yalnızca `Available` durumundayken alınabilir.
* Bir Issue için yalnızca tek accepted active contribution kabul edilir.
* Development contributor'ın kendi fork'undaki branch üzerinde yapılmalıdır.
* Review feedback yeni PR açmadan aynı branch üzerinde uygulanmalıdır.
* PR açıklamasında `Closes #<issue-number>` kullanılmalıdır.
* Upstream `main` branch'e doğrudan push yapılmamalıdır.

## AI Usage

AI araçlarının kullanımı serbesttir.

Contributor:

* üretilen kodu anlamalı,
* doğrulamalı,
* test etmeli,
* teknik kararlarını açıklayabilmelidir.

## Technical Notes

Gerekli teknik bağlam, bağımlılıklar veya dikkat edilmesi gereken noktalar.

## Test Requirements

* [ ] Proje build edilmeli.
* [ ] İlgili otomatik testler çalışmalı.
* [ ] Yeni davranış mümkünse otomatik test ile doğrulanmalı.

## Definition of Done

* [ ] Acceptance Criteria tamamlandı.
* [ ] Allowed Scope dışına çıkılmadı.
* [ ] Build başarılı.
* [ ] Testler başarılı.
* [ ] PR açıldı.
* [ ] PR açıklamasında Issue bağlandı.
* [ ] Review feedback'leri giderildi.
