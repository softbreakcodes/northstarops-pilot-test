# 09 — Review, Revision ve Merge Akışı

## Amaç

Contributor'ın review feedback'i sonrası yeni PR açmadan aynı contribution üzerinde ilerlemesini sağlamak.

## Akış

```text
Draft PR
→ Ready for Review
→ In Review
→ Request Changes
→ aynı branch'e yeni commit
→ aynı PR güncellenir
→ yeniden review
→ Approve
→ Squash Merge
→ Issue Closed
→ Done
```

## Kural: aynı branch / aynı PR

Review sonucu değişiklik istenirse contributor:

- yeni Issue açmaz,
- yeni PR açmaz,
- aynı branch üzerinde düzeltme yapar,
- commit + push ile mevcut PR'ı günceller.

Bu yaklaşım review context'ini, diff geçmişini ve Issue bağlantısını korur.

## Branch adı

Branch isminde Issue numarası bulunması önerilir:

```text
feature/123-short-description
fix/123-short-description
```

## Merge

`main` yalnızca Pull Request üzerinden güncellenir. Production hedefinde:

- en az 1 approval,
- unresolved conversation olmaması,
- stale approval'ın yeni push sonrası geçersizleşmesi,
- yalnızca squash merge

kullanılması önerilir.

## Issue kapanışı

PR body:

```text
Closes #123
```

şeklinde olduğunda merge sonrası bağlı Issue GitHub tarafından otomatik kapanır.

## Project Done

Issue kapandıktan sonra Project kartının `Done` olması sağlanmalıdır. Bu işlem Project'in yerleşik automation'ı ile yapılabilir. Production Project kurulurken `Item closed → Status Done` automation'ı etkinleştirilmelidir.

## Pilot uçtan uca doğrulaması

Pilot lifecycle testinde:

- contributor claim etti,
- Draft PR açtı,
- Ready for Review oldu,
- maintainer Request Changes verdi,
- contributor aynı branch ve PR üzerinde revision yaptı,
- approval sonrası squash merge gerçekleşti,
- closing reference ile Issue kapandı.

## PASS kriteri

Review feedback sonrasında ikinci PR oluşmadan aynı PR merge edilir ve bağlı Issue kapanır.
