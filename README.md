# NorthStarOps Pilot Test

`northstarops-pilot-test`, NorthStarOps için tasarlanan GitHub-native contribution ve engineering governance modelinin gerçek GitHub davranışlarıyla test edildiği pilot repository'dir.

Bu repository'nin amacı uygulama geliştirmekten çok, daha sonra `softbreak/northstarops` üzerinde kullanılacak contributor lifecycle'ını doğrulamaktır.

> Bu bir pilot/test ortamıdır. Production NorthStarOps kurulumu için `docs/` altındaki runbook kaynak gerçeklik olarak kullanılmalıdır.

## Amaç

Pilot şu soruları gerçek fork, Issue, Pull Request, GitHub Actions ve Project V2 olayları üzerinden doğrular:

- Bir contributor bir Issue'yu güvenli biçimde nasıl claim eder?
- Aynı contributor'ın birden fazla işi bloke etmesi nasıl engellenir?
- Claim edilen fakat başlanmayan işler nasıl tekrar `Available` yapılır?
- Bir PR'ın yalnızca doğru Issue ve doğru claimant tarafından açılması nasıl zorunlu tutulur?
- Draft PR, review ve Project V2 status'ları nasıl senkron tutulur?
- Review feedback sonrası aynı branch ve aynı PR üzerinden revision nasıl yürütülür?
- Tek maintainer/reviewer darboğazı ChatGPT destekli Issue-context review ile nasıl azaltılır?

## Hedef lifecycle

```text
Backlog
→ Available
→ /claim
→ Claimed
→ Draft PR within 24h
→ Ready for Review
→ In Review
→ ChatGPT-assisted Review
→ REQUEST_CHANGES (gerekirse)
→ same branch / same PR revision
→ re-review
→ APPROVE recommendation
→ maintainer merge decision
→ Squash Merge
→ Issue Closed
→ Done
```

Timeout yolu:

```text
Claimed
+ 24 saat içinde qualifying PR yok
→ claim kaldır
→ Available
```

## Contributor modeli

Contributor'ın upstream repository için write yetkisine ihtiyacı yoktur.

```text
Public repo
→ Available Issue seç
→ /claim
→ Fork
→ Clone
→ Issue numaralı branch
→ Commit
→ Push fork
→ Draft Pull Request
→ Closes #<issue-number>
→ Ready for Review
→ review / revision
→ squash merge
```

Temel kurallar:

- Yalnızca `Available` Issue claim edilebilir.
- Bir contributor aynı anda yalnızca bir aktif claim taşıyabilir.
- Claim sonrası 24 saat içinde bağlı Draft PR açılmalıdır.
- Her PR tam olarak bir Issue'ya closing reference ile bağlanmalıdır.
- PR author, Issue assignee ile aynı kişi olmalıdır.
- Issue scope'u dışına çıkılmamalıdır.
- Review feedback yeni PR açmadan aynı branch ve aynı PR üzerinde uygulanmalıdır.
- Upstream `main` branch'e doğrudan push yapılmaz.
- AI kullanımı serbesttir; contributor üretilen işi anlamak, doğrulamak, test etmek ve açıklamakla sorumludur.

Ayrıntılar için [`CONTRIBUTING.md`](CONTRIBUTING.md) ve standart Issue template'e bakın.

## GitHub governance bileşenleri

Pilot `main` üzerinde doğrulanan temel workflow'lar:

```text
.github/workflows/claim-issue.yml
.github/workflows/claim-timeout.yml
.github/workflows/pr-admission.yml
.github/workflows/pr-review-status.yml
```

### Claim

`/claim` başarılı olduğunda:

```text
assignee = actor
status: available → status: claimed
Project Status = Claimed
```

### Tek aktif claim

Bir contributor'ın başka açık `status: claimed` Issue'su varsa yeni claim reddedilir.

### 24 saat timeout

Deadline içinde qualifying PR yoksa:

```text
assignee kaldır
status: claimed kaldır
status: available ekle
Project Status = Available
```

### PR admission

Bir Pull Request'in kabul edilmesi için en az:

- tam olarak bir closing reference,
- açık ve claim edilmiş Issue,
- assignee ile PR author eşleşmesi

beklenir. Geçersiz PR açıklayıcı mesajla kapatılır.

### Review status sync

```text
Draft → Ready for Review
Issue label = status: in-review
Project Status = In Review
```

PR tekrar Draft'a çevrilirse lifecycle tekrar `Claimed` durumuna döner.

## ChatGPT destekli PR review

NorthStarOps'ta tek human reviewer bulunması nedeniyle ilk aşamada her PR'ın tamamen manuel incelenmesi hedeflenmez.

PR `Ready for Review` olduğunda maintainer ChatGPT'ye PR numarasını veya linkini vererek Issue-context review başlatabilir.

ChatGPT review sırasında en az şunları değerlendirir:

```text
ISSUE ALIGNMENT
SCOPE
ACCEPTANCE CRITERIA
TESTS
CODE QUALITY
RISK
```

ve sonucu:

```text
APPROVE
veya
REQUEST_CHANGES
```

önerisine indirger.

ChatGPT gerektiğinde ve maintainer açıkça istediğinde GitHub'a review/comment bırakabilir. Final merge yetkisi maintainer'da kalır.

İlk aşamada CodeRabbit veya başka bir üçüncü taraf AI reviewer zorunlu değildir.

Detay: [`docs/13-chatgpt-assisted-pr-review.md`](docs/13-chatgpt-assisted-pr-review.md)

## Issue ve Project lifecycle

Issue lifecycle label'ları:

```text
status: available
status: claimed
status: in-review
```

Project V2 status'ları:

```text
Backlog
Available
Claimed
In Review
Done
```

Issue label'ı ve Project V2 Status birlikte güncellenmelidir. Pilot sırasında bu iki state'in drift etmesi gerçek testlerde gözlemlendiği için workflow'larda rollback/fail-closed yaklaşımı kullanılmıştır.

## Repository ve izin modeli

Pilot modelinde:

- repository public,
- default branch `main`,
- Issues ve Projects açık,
- forking açık,
- contributor upstream'e doğrudan push etmez,
- contribution fork + Pull Request ile gelir.

Production hedefinde `main` için:

- Pull Request required,
- 1 approval hedefi,
- stale approval dismissal,
- conversation resolution,
- squash-only merge,
- force push blocked,
- branch deletion blocked

önerilmektedir.

Pilotun son doğrulanan ruleset durumunda required approval sayısının `0` olduğu ayrıca dokümante edilmiştir; `1 approval` production hedefidir.

## Project V2 entegrasyonu

Workflow'ların Project V2 Status alanını değiştirebilmesi için pilotta repository secret olarak:

```text
NORTHSTAROPS_PROJECT_TOKEN
```

kullanılmıştır.

Token değeri hiçbir repository dosyasına veya log'a yazılmamalıdır.

## Dokümantasyon / rebuild runbook

Gerçek NorthStarOps kurulurken dokümanlar sırayla uygulanmalıdır:

1. [Repository ve İzin Modeli](docs/01-repository-and-permissions.md)
2. [GitHub Project V2 ve Lifecycle Status'ları](docs/02-project-v2-and-lifecycle-statuses.md)
3. [Issue Template ve Lifecycle Label'ları](docs/03-issue-template-and-labels.md)
4. [`/claim` Workflow](docs/04-claim-workflow.md)
5. [Tek Aktif Claim Kuralı](docs/05-single-active-claim.md)
6. [24 Saat Claim Timeout](docs/06-claim-timeout.md)
7. [Pull Request Admission](docs/07-pr-admission.md)
8. [Draft PR ve Review Status Senkronizasyonu](docs/08-review-status-sync.md)
9. [Review, Revision ve Merge Akışı](docs/09-review-revision-and-merge.md)
10. [Uçtan Uca Test Senaryoları](docs/10-end-to-end-test-scenarios.md)
11. [Production Migration Checklist](docs/11-production-migration-checklist.md)
12. [Pilot Bulguları ve Test Edilmiş Düzeltmeler](docs/12-pilot-findings-and-tested-fixes.md)
13. [ChatGPT Destekli Pull Request Review Modeli](docs/13-chatgpt-assisted-pr-review.md)

Runbook index: [`docs/README.md`](docs/README.md)

## Pilot sırasında doğrulanan önemli davranışlar

- `/claim` ile assignee + label + Project Status birlikte güncellendi.
- İkinci aktif claim reddedildi.
- 24 saat timeout modeli manuel `timeout_hours = 0` testiyle doğrulandı.
- Timeout sonrası Issue ve Project birlikte `Available` durumuna döndü.
- Geçersiz PR admission senaryoları reddedildi.
- Draft → Ready for Review geçişinde Issue ve Project `In Review` senkronizasyonu doğrulandı.
- Request Changes sonrası contributor aynı branch ve aynı PR üzerinde revision yaptı.
- Squash merge sonrası closing reference ile Issue kapandı.
- Project mutation başarısızlıklarına karşı rollback/fail-closed yaklaşımı eklendi.

Pilot sırasında bulunan hatalar ve düzeltmeler için [`docs/12-pilot-findings-and-tested-fixes.md`](docs/12-pilot-findings-and-tested-fixes.md) dosyasına bakın.

## Production'a geçiş

Bu repository doğrudan production değildir. Hedef, doğrulanan yapıyı daha sonra örneğin:

```text
softbreak/northstarops
```

üzerinde yeniden kurmaktır.

Production contributor duyurusu yapılmadan önce [`docs/10-end-to-end-test-scenarios.md`](docs/10-end-to-end-test-scenarios.md) ve [`docs/11-production-migration-checklist.md`](docs/11-production-migration-checklist.md) içindeki release/launch gate'ler tamamlanmalıdır.
