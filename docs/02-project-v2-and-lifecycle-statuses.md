# 02 — GitHub Project V2 ve Lifecycle Status'ları

## Amaç

Issue'ların yalnızca label ile değil, Project V2 board üzerinde de aynı lifecycle durumunu göstermesini sağlamak.

## Gerekli Project

Production için organization-level bir Project V2 oluştur:

```text
NorthStarOps
```

Pilot ortamında proje adı `NorthstarOps Pilot Test` idi. Workflow dosyalarındaki `PROJECT_TITLE` production adına göre değiştirilmelidir.

## Status alanı

Project içindeki `Status` single-select alanında en az şu seçenekler bulunmalıdır:

```text
Backlog
Available
Claimed
In Review
Done
```

Akış:

```text
Backlog → Available → Claimed → In Review → Done
                   ↘ timeout ↘
                     Available
```

PR tekrar Draft'a çevrilirse:

```text
In Review → Claimed
```

## Neden Project V2 senkronizasyonu gerekli?

Pilot testlerde yalnızca Issue label'ını değiştirmek yeterli olmadı. Issue `status: in-review` olsa bile board kartı `Claimed` olarak kalabildi. Bu nedenle workflow'lar label ve Project `Status` alanını birlikte güncelleyecek şekilde geliştirildi.

## Workflow beklentisi

- `/claim` başarılı → Project Status `Claimed`
- Ready for Review → Project Status `In Review`
- Convert to Draft → Project Status `Claimed`
- Claim timeout → Project Status `Available`
- Merge/Issue close sonrası → board otomasyonu veya manuel lifecycle ile `Done`

## Project token

Project V2 GraphQL mutation'ları için:

```text
NORTHSTAROPS_PROJECT_TOKEN
```

secret'ı kullanılır.

## Kritik öğrenme

Project item lookup için `gh api --jq --arg` kombinasyonuna güvenilmemelidir. Pilotta bu kullanım hata verdi. JSON önce alınmalı, gerekli filtreleme lokal `jq --arg` ile yapılmalıdır.

## PASS kriteri

Bir test Issue için aşağıdaki geçişler board üzerinde görünür:

```text
Available → Claimed → In Review
```

ve timeout testinde:

```text
Claimed → Available
```
