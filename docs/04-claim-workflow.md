# 04 — `/claim` Workflow

## Amaç

Contributor'ın `Available` bir Issue'yu yorum ile sahiplenebilmesini ve bu sahipliğin Issue + Project V2 üzerinde atomik biçimde görünmesini sağlamak.

## Workflow dosyası

```text
.github/workflows/claim-issue.yml
```

## Tetikleyici

```text
issue_comment → created
```

Workflow yalnızca şu koşullarda çalışmalıdır:

- yorum bir PR üzerinde değil Issue üzerindedir,
- Issue açıktır,
- yorum metni tam olarak `/claim`'dir.

## Kabul kontrolleri

Claim öncesi sırayla kontrol edilir:

1. Issue `status: available` label'ına sahip mi?
2. Issue üzerinde mevcut assignee var mı?
3. Actor'ın başka açık `status: claimed` Issue'su var mı?
4. GitHub actor'ın Issue'ya assign edilmesine izin veriyor mu?
5. Project token mevcut mu?
6. Project V2, `Status` field ve `Claimed` option bulunabiliyor mu?

## Başarılı claim mutation'ları

```text
assignee = actor
status: available kaldır
status: claimed ekle
Project Status = Claimed
```

Sonra bot yorum bırakır:

```text
✅ @actor bu Issue'yu claim etti. 24 saat içinde bağlı bir Draft PR açmalısın.
```

Yorum ayrıca workflow'un timeout sürecinde claim başlangıcını güvenilir biçimde bulabilmesi için gizli marker içermelidir:

```text
<!-- northstarops-claim -->
```

## Atomiklik / rollback

Project Status `Claimed` yapılamazsa claim başarılı sayılmamalıdır. Workflow:

- `status: available` label'ını geri ekler,
- `status: claimed` label'ını kaldırır,
- assignee'ı kaldırır,
- hata yorumu bırakır.

Bu davranış pilotta özellikle Project ve Issue state drift'ini engellemek için eklendi.

## Pilot sırasında bulunan hata

Claim confirmation mesajında bir aşamada literal `@${ACTOR}` görünüyordu. Mesaj `printf` ile oluşturularak gerçek kullanıcı adının expand edilmesi sağlandı.

## PASS testi

Bir test Issue:

```text
status: available
```

ile başlatılır. Contributor `/claim` yazar.

Beklenen:

```text
assignee = contributor
label = status: claimed
Project Status = Claimed
bot mesajı gerçek kullanıcı adını gösterir
```
