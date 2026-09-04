# 12 — Pilot Bulguları ve Test Edilmiş Düzeltmeler

## Amaç

Gerçek NorthStarOps kurulurken pilot sırasında yaşanan hataların tekrar edilmesini önlemek.

## 1. Claim mesajında literal kullanıcı adı

Problem:

```text
@${ACTOR}
```

gerçek kullanıcı adına dönüşmeden bot mesajında görünüyordu.

Düzeltme: message body `printf` ile oluşturuldu ve shell variable expansion doğrulandı.

## 2. Claim timeout 48 saatten 24 saate indirildi

İlk tasarım 48 saatti. Governance kararı sonradan 24 saat olarak netleştirildi. Production dokümanı ve workflow default'u 24 saat olmalıdır.

## 3. Issue label ile Project Status drift etti

İlk review status implementasyonu yalnızca Issue label'ını değiştiriyordu. Sonuç:

```text
Issue label = In Review
Project Status = Claimed
```

Düzeltme: `pr-review-status.yml` aynı event içinde Project V2 Status mutation da yapacak şekilde değiştirildi.

## 4. `/claim` sonrası Project kartı Claimed'a geçmiyordu

Claim ilk sürümde assignee ve label'ı doğru güncellese de Project V2 Status ayrı kaldı.

Düzeltme: claim workflow Project item lookup/add + `Status = Claimed` mutation yapıyor. Mutation başarısızsa Issue claim state rollback ediliyor.

## 5. Project item lookup CLI hatası

Geçersiz `gh api --jq --arg` kullanımı `accepts 1 arg(s), received 4` benzeri hata üretti.

Düzeltme:

```text
API JSON al → local jq --arg ile filtrele
```

## 6. Timeline API method hatası

Claim timeout testinde:

```text
gh: Not Found (HTTP 404)
invalid control character in URL
```

hatası görüldü.

Kök neden: `gh api --paginate` çağrısında `-f per_page=100` bulunurken açıkça GET verilmemesi.

Düzeltme:

```text
-X GET
```

kullanıldı.

## 7. Hardcoded closing-reference örneği

PR admission reject mesajında test geçmişinden kalan:

```text
Closes #17
```

örneği vardı.

Düzeltme: gerçek Issue zannedilmemesi için nötr:

```text
Closes #123
```

kullanıldı.

## 8. Claim timeout Project sync

İlk timeout davranışı Issue label/assignee'ı Available'a döndürüyordu ancak Project V2 kartı drift edebiliyordu.

Düzeltme: timeout sırasında Project Status da `Available` yapılıyor; mutation başarısızsa claim state rollback ediliyor.

## 9. Review revision aynı PR üzerinde doğrulandı

Request Changes sonrasında contributor aynı branch'e yeni commit gönderdi. Aynı PR üzerinde re-review ve approval ile squash merge yapıldı. Yeni PR açmaya gerek olmadığı uçtan uca doğrulandı.

## 10. Connector / GitHub UI görünürlük farkı

Bir PR check sonucu connector üzerinden anlık görünmeyebilirken GitHub UI'da check başarılı görünmüştür. Operasyonel karar verirken kritik merge gate için GitHub'ın gerçek PR Checks görünümü gerekirse son doğrulama kaynağı olarak kullanılmalıdır.

## Production ilkesi

Bu bulguların ortak sonucu:

```text
Issue state + Project state birlikte güncellenmeli.
Kısmi mutation başarısızlığında fail-closed / rollback uygulanmalı.
Her governance kuralı farklı kullanıcı hesabıyla gerçek GitHub event'i üzerinden test edilmeli.
```
