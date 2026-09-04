# 03 — Issue Template ve Lifecycle Label'ları

## Amaç

Her işin aynı kalite kapılarından geçmesini ve contributor'ın Issue'yu açtığı anda çalışma kurallarını görmesini sağlamak.

## Standart Issue template

Dosya:

```text
.github/ISSUE_TEMPLATE/work-item.md
```

Template şu bölümleri içermelidir:

- Business Problem
- Expected Behaviour
- Acceptance Criteria
- Allowed Scope
- Do Not Change
- Contribution Rules
- AI Usage
- Technical Notes
- Test Requirements
- Definition of Done

## Contribution Rules içinde zorunlu metinler

- Issue yalnızca `Available` durumundayken alınabilir.
- Issue'yu almak için yorum olarak tam olarak `/claim` yazılır.
- Claim sonrası 24 saat içinde bağlı bir Draft PR açılır.
- 24 saatte bağlı PR yoksa claim düşer ve Issue tekrar `Available` olur.
- Bir Issue için tek accepted active contribution vardır.
- Development contributor'ın kendi fork'undaki branch üzerinde yapılır.
- Review feedback aynı branch ve aynı PR üzerinde uygulanır.
- PR body tam olarak bir closing reference içerir: `Closes #<issue-number>`.
- Upstream `main` branch'e doğrudan push yapılmaz.
- AI kullanımı serbesttir; contributor üretilen işi anlamak, test etmek ve açıklamakla sorumludur.

## Lifecycle label'ları

En az şu label'ları oluştur:

```text
status: available
status: claimed
status: in-review
```

Pilot workflow'larda kullanılan anlamlar:

- `status: available`: claim edilebilir iş
- `status: claimed`: aktif contributor tarafından sahiplenilmiş iş
- `status: in-review`: bağlı PR review için hazır

`Backlog` ve `Done` Project V2 status olarak yönetilebilir; label olmak zorunda değildir.

## CONTRIBUTING.md

Root'ta ayrıca `CONTRIBUTING.md` bulunmalı ve şu contributor yolunu özetlemelidir:

```text
Available Issue
→ /claim
→ fork
→ branch
→ Draft PR
→ Ready for Review
→ review
→ aynı PR üzerinde revision
→ approval
→ squash merge
```

## PASS kriteri

Yeni bir Work Item oluşturulduğunda scope, test, AI ve claim kuralları contributor'a görünür; lifecycle label'ları workflow'lar tarafından bulunabilir durumdadır.
