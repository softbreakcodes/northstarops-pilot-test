# 01 — Repository ve İzin Modeli

## Amaç

Bu adım gerçek NorthStarOps repository'sini `softbreak` organizasyonunda oluşturur ve katkı modelinin güvenlik sınırlarını belirler.

## Pilot ortamında doğrulanan yapı

Pilot repository `softbreakcodes/northstarops-pilot-test` public olarak çalıştı. Default branch `main`, Issues ve Projects açık, fork edilebilir durumda. Katılımcı akışı upstream'e doğrudan push yerine fork + Pull Request üzerinden yürütüldü.

## Production hedefi

Önerilen gerçek repository:

```text
softbreak/northstarops
```

Repository oluştururken:

- Visibility: `Public`
- Default branch: `main`
- Issues: Enabled
- Projects: Enabled
- Discussions: ihtiyaca göre Enabled
- Forking: Enabled
- Wiki: kapalı tutulabilir
- Direct contributor push: verilmemeli

## Roller

### Engineering Lead / Maintainer

- Repository administration
- Issue oluşturma ve backlog yönetimi
- Review ve merge
- Project V2 yönetimi
- Repository secret yönetimi
- Ruleset yönetimi

### Contributor

Katılımcının upstream repository için write yetkisine ihtiyacı yoktur. Akış:

```text
Public repo → Fork → Clone → Branch → Commit → Push fork → Pull Request upstream
```

Bu model kalabalık contributor grubunda upstream branch çakışmasını ve doğrudan `main` değişikliklerini sınırlar.

## Repository token / secret

Project V2 alanını GitHub Actions içinden değiştirmek için standart `GITHUB_TOKEN` yeterli olmayabilir. Pilot sistemde repository secret olarak şu isim kullanıldı:

```text
NORTHSTAROPS_PROJECT_TOKEN
```

Token'ın minimum olarak repository ve Project V2 üzerinde gerekli okuma/yazma yetkilerine sahip olması gerekir. Token değeri hiçbir dokümana, workflow dosyasına veya log çıktısına yazılmamalıdır.

Production kurulumunda aynı secret adı korunursa workflow dosyaları daha az değişiklikle taşınabilir.

## Merge ayarları

Pilot ruleset fiilen `main` için PR zorunluluğu, conversation resolution, force-push engeli, branch deletion engeli ve yalnızca squash merge kuralını uyguladı. Pilotun son doğrulanan ruleset durumunda required approving review count `0` idi.

Production için önerilen hedef:

- Pull Request required
- Required approvals: `1`
- Dismiss stale approvals on push: Enabled
- Require conversation resolution: Enabled
- Allowed merge method: yalnızca `Squash`
- Force push: blocked
- Branch deletion: blocked
- Bypass: mümkünse none

Not: `1 approval` production governance hedefidir; pilotun son ruleset görüntüsünde approval sayısı `0` olarak gözlemlendi. Gerçek kurulumda bu değer bilinçli olarak `1` yapılmalıdır.

## PASS kriteri

- Public repository oluşturuldu.
- `main` default branch.
- Contributor upstream'e push etmek zorunda değil.
- Fork + PR akışı mümkün.
- `NORTHSTAROPS_PROJECT_TOKEN` secret'ı tanımlandı.
- `main` ruleset aktif.
