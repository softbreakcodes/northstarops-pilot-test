# 05 — Tek Aktif Claim Kuralı

## Amaç

Bir contributor'ın aynı anda birden fazla Issue'yu bloke etmesini engellemek.

## Kural

Bir kullanıcı aynı anda yalnızca bir açık `status: claimed` Issue'ya sahip olabilir.

Claim workflow şu aramayı yapmalıdır:

```text
repo:<owner>/<repo> is:issue is:open label:"status: claimed" assignee:<actor>
```

Sonuç `0`'dan büyükse yeni claim reddedilir.

## Red mesajı

Contributor'a açık ve eyleme dönük mesaj verilir:

```text
Zaten aktif bir claim'in var. Yeni bir Issue claim etmeden önce mevcut çalışmanı tamamla veya claim'i bırak.
```

## Neden gerekli?

Bu kural özellikle 15–100 contributor ölçeğinde backlog'un birkaç kişi tarafından tutulmasını engeller. Her aktif contributor bir işi tamamlamaya veya bırakmaya teşvik edilir.

## Pilot testi

İlk Issue üzerinde aktif claim varken ikinci `Available` Issue için `/claim` denendi. Workflow ikinci claim'i reddetti ve mevcut claim'i korudu.

## PASS kriteri

- Kullanıcı A bir Issue'yu claim eder.
- Kullanıcı A ikinci Available Issue'ya `/claim` yazar.
- İkinci Issue Available kalır.
- Assignee atanmaz.
- Bot aktif claim bulunduğunu bildirir.
