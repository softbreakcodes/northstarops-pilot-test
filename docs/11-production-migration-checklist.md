# 11 — Production Migration Checklist

## Hedef

Pilot altyapısını `softbreak` organizasyonunda gerçek NorthStarOps repository'sine taşımak.

Hedef repository örneği:

```text
softbreak/northstarops
```

## 1. Repository

- [ ] Public repository oluştur.
- [ ] Default branch `main`.
- [ ] Issues Enabled.
- [ ] Projects Enabled.
- [ ] Forking Enabled.
- [ ] Contributor'lara upstream write yetkisi verme.
- [ ] Root `README.md`, `CONTRIBUTING.md` ve uygun `.gitignore` ekle.

## 2. Project V2

- [ ] Organization seviyesinde `NorthStarOps` Project V2 oluştur.
- [ ] `Status` alanında `Backlog`, `Available`, `Claimed`, `In Review`, `Done` oluştur.
- [ ] Closed item → Done automation'ını ayarla.
- [ ] Repository'yi Project ile ilişkilendir.

## 3. Labels

- [ ] `status: available`
- [ ] `status: claimed`
- [ ] `status: in-review`

İhtiyaca göre priority/type label'ları ayrıca eklenebilir; lifecycle workflow'larından ayrı tutulmalıdır.

## 4. Issue Template

- [ ] `.github/ISSUE_TEMPLATE/work-item.md` kopyala/uyarla.
- [ ] `/claim` kuralını koru.
- [ ] 24 saat Draft PR kuralını koru.
- [ ] `Closes #<issue-number>` kuralını koru.
- [ ] Same branch / same PR revision kuralını koru.

## 5. Workflows

Pilot `main` üzerindeki şu dört dosyayı production'a taşı:

```text
.github/workflows/claim-issue.yml
.github/workflows/claim-timeout.yml
.github/workflows/pr-admission.yml
.github/workflows/pr-review-status.yml
```

Taşıma sonrası `PROJECT_TITLE` değerlerini production Project adına göre güncelle.

## 6. Secret

Repository Actions secret:

```text
NORTHSTAROPS_PROJECT_TOKEN
```

- [ ] Project V2 GraphQL read/write için yetkili token oluştur.
- [ ] Secret olarak kaydet.
- [ ] Token'ı repo dosyalarına yazma.
- [ ] Test loglarında token sızıntısı olmadığını doğrula.

## 7. Ruleset

`main` için active ruleset oluştur.

Production hedefi:

- [ ] Pull Request required.
- [ ] 1 approval required.
- [ ] Dismiss stale approvals on push.
- [ ] Conversation resolution required.
- [ ] Force push blocked.
- [ ] Branch deletion blocked.
- [ ] Allowed merge method: Squash only.
- [ ] Bypass yok veya yalnızca acil kontrollü admin süreci.

## 8. Repository merge settings

Repository genel ayarlarında merge commit/rebase açık olsa bile ruleset `main` için yalnızca squash'a izin verebilir. Yine de production'da kafa karışıklığını azaltmak için mümkünse repository ayarlarında da yalnızca squash açık bırak.

## 9. ChatGPT-assisted review operating model

İlk aşamada CodeRabbit veya başka bir üçüncü taraf AI reviewer zorunlu değildir.

- [ ] `13-chatgpt-assisted-pr-review.md` modelini production operasyonuna dahil et.
- [ ] PR `Ready for Review` olduğunda maintainer ChatGPT'ye PR numarası/linki ile review başlatır.
- [ ] Review bağlı Issue + diff + changed files + tests/checks bağlamında yapılır.
- [ ] Standart çıktı `ISSUE ALIGNMENT`, `SCOPE`, `ACCEPTANCE CRITERIA`, `TESTS`, `CODE QUALITY`, `RISK` ve `RECOMMENDATION` içerir.
- [ ] ChatGPT GitHub'a review bırakacaksa bu maintainer'ın açık talebiyle yapılır.
- [ ] Final merge authority maintainer'da kalır.
- [ ] Background/automatic review gerektiğinde bunun ayrı bir integration/automation ihtiyacı olduğu kabul edilir.

## 10. Smoke test

Gerçek contributor yerine test hesabı ile `10-end-to-end-test-scenarios.md` içindeki testleri çalıştır.

En az şu zincir PASS olmalı:

```text
Available
→ /claim
→ Claimed
→ Draft PR within 24h
→ PR Admission PASS
→ Ready for Review
→ In Review
→ ChatGPT-assisted Review
→ Request Changes (gerekirse)
→ same PR revision
→ re-review
→ maintainer merge kararı
→ Squash Merge
→ Issue Closed
→ Done
```

Ayrıca:

```text
Claimed + no PR → 24h timeout → Available
```

PASS olmalı.

## 11. Launch gate

Contributor duyurusu yapmadan önce:

- [ ] Açık test Issue/PR kalmadı.
- [ ] Board test kartlarından temiz.
- [ ] Workflow run'ları green.
- [ ] Secret doğru.
- [ ] Project Status isimleri workflow ile birebir aynı.
- [ ] Issue template doğru repo/project isimlerini kullanıyor.
- [ ] CONTRIBUTING.md public katılımcı akışını anlatıyor.
- [ ] En az bir farklı GitHub hesabıyla fork → claim → PR akışı başarıyla tamamlandı.
- [ ] En az bir gerçek test PR'ı ChatGPT tarafından bağlı Issue bağlamında review edildi.
- [ ] ChatGPT review sonrası same-PR revision ve re-review akışı doğrulandı.

Bu gate tamamlanmadan gerçek backlog `Available` durumuna açılmamalıdır.
