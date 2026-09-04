# 08 — Draft PR ve Review Status Senkronizasyonu

## Amaç

Contributor PR'ı review için hazır hale getirdiğinde Issue ve Project V2 lifecycle'ın birlikte `In Review` durumuna geçmesini sağlamak.

## Workflow dosyası

```text
.github/workflows/pr-review-status.yml
```

## Tetikleyiciler

```text
pull_request_target:
  ready_for_review
  converted_to_draft
```

## Ready for Review

PR Draft durumundan çıkarıldığında workflow:

1. PR body'den unique closing Issue'yu bulur.
2. Issue açık mı kontrol eder.
3. Issue assignee ile PR author eşleşmesini doğrular.
4. Project V2 item'ını bulur veya ekler.
5. Project Status'ı `In Review` yapar.
6. `status: in-review` label'ını ekler.
7. `status: claimed` label'ını kaldırır.

Beklenen sonuç:

```text
Claimed → In Review
```

## Convert to Draft

PR tekrar Draft'a alınırsa:

```text
In Review → Claimed
```

Workflow Project Status'ı `Claimed` yapar, `status: claimed` label'ını geri ekler ve `status: in-review` label'ını kaldırır.

## Pilot sırasında bulunan boşluk

İlk sürüm yalnızca Issue label'ını değiştiriyordu. Testte Issue `status: in-review` olurken Project kartının `Claimed` kaldığı görüldü. Bu nedenle Project V2 mutation aynı workflow'a eklendi.

## PASS testi

1. Available Issue claim edilir.
2. Draft PR açılır.
3. PR Ready for Review yapılır.
4. Issue label = `status: in-review`.
5. Project Status = `In Review`.
6. PR tekrar Draft yapılırsa ikisi de `Claimed` durumuna döner.
