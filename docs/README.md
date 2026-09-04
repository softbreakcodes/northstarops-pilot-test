# NorthStarOps GitHub Governance Rebuild Runbook

Bu klasör `softbreakcodes/northstarops-pilot-test` üzerinde oluşturulan ve test edilen contribution/governance altyapısını, daha sonra `softbreak` organizasyonunda gerçek NorthStarOps repository'sinde yeniden kurmak için hazırlanmıştır.

Dokümanları sırayla uygula:

1. [01 — Repository ve İzin Modeli](01-repository-and-permissions.md)
2. [02 — GitHub Project V2 ve Lifecycle Status'ları](02-project-v2-and-lifecycle-statuses.md)
3. [03 — Issue Template ve Lifecycle Label'ları](03-issue-template-and-labels.md)
4. [04 — `/claim` Workflow](04-claim-workflow.md)
5. [05 — Tek Aktif Claim Kuralı](05-single-active-claim.md)
6. [06 — 24 Saat Claim Timeout](06-claim-timeout.md)
7. [07 — Pull Request Admission](07-pr-admission.md)
8. [08 — Draft PR ve Review Status Senkronizasyonu](08-review-status-sync.md)
9. [09 — Review, Revision ve Merge Akışı](09-review-revision-and-merge.md)
10. [10 — Uçtan Uca Test Senaryoları](10-end-to-end-test-scenarios.md)
11. [11 — Production Migration Checklist](11-production-migration-checklist.md)
12. [12 — Pilot Bulguları ve Test Edilmiş Düzeltmeler](12-pilot-findings-and-tested-fixes.md)

## Kaynak gerçeklik

Bu runbook pilot repository'nin `main` branch'inde doğrulanan dört workflow'u esas alır:

```text
.github/workflows/claim-issue.yml
.github/workflows/claim-timeout.yml
.github/workflows/pr-admission.yml
.github/workflows/pr-review-status.yml
```

Pilot ortama özgü değerler production'a birebir taşınmamalıdır. Özellikle:

```text
softbreakcodes/northstarops-pilot-test
NorthstarOps Pilot Test
softbreak-user
```

değerleri gerçek organization/repository/project ve test contributor değerleriyle değiştirilmelidir.

## Hedef lifecycle

```text
Backlog
→ Available
→ /claim
→ Claimed
→ Draft PR within 24h
→ Ready for Review
→ In Review
→ Review
→ Changes Requested (gerekirse)
→ same branch / same PR revision
→ Approve
→ Squash Merge
→ Issue Closed
→ Done
```

Timeout yolu:

```text
Claimed + 24 saat içinde qualifying PR yok
→ claim kaldır
→ Available
```

Bu klasör yeni production kurulumunun operasyon sırasıdır; workflow dosyalarının kendisi için pilot `main` branch kaynak referans olarak kullanılmalıdır.
