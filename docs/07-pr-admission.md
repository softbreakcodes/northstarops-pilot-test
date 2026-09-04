# 07 — Pull Request Admission

## Amaç

Bir Pull Request'in yalnızca geçerli claim sahibi tarafından ve tam olarak bir Issue için açılabilmesini sağlamak.

## Workflow dosyası

```text
.github/workflows/pr-admission.yml
```

## Tetikleyici

```text
pull_request_target
  opened
  reopened
  edited
```

## Closing reference parser

PR body içinden aşağıdaki türde referanslar aranır:

```text
Closes #123
Fixes #123
Resolves #123
```

Tam olarak bir unique Issue numarası bulunmalıdır.

## Admission kontrolleri

Sıra:

1. PR body tam olarak bir closing reference içeriyor mu?
2. Bağlı Issue repository'de var mı?
3. Issue açık mı?
4. Issue `status: claimed` durumunda mı?
5. Issue üzerinde assignee var mı?
6. Assignee ile PR author aynı kullanıcı mı?

## Reject davranışı

Kontrollerden biri geçmezse workflow:

- PR'a açıklayıcı comment yazar,
- PR'ı kapatır,
- neden reddedildiğini log'a yazar.

Örnek nötr yardım metni:

```text
Closes #123
```

Gerçek Issue numarası contributor tarafından yazılır.

## Güvenlik nedeni

GitHub yerleşik olarak aynı Issue için farklı kişilerden gelen birden fazla PR'ı contributor claim modeline göre engellemez. Admission workflow bu governance boşluğunu kapatır.

## Pilot testleri

- Missing closing reference reddedildi.
- Claimed olmayan Issue'ya PR reddedildi.
- Başkasının claim ettiği Issue için PR reddedildi.
- Claim sahibi doğru `Closes #N` ile PR açtığında admission geçti.

## PASS kriteri

Geçerli claimant'ın doğru bağlı PR'ı açık kalır; geçersiz PR otomatik kapanır.
