# [Título do Estudo]

## [Seção 1]

[Descrição breve sobre o tópico abordado.]

```csharp
public ResultViewModel<UserViewModel> GetById(int id)
{
    var result = _context.Users
        .Include(u => u.Skills)
            .ThenInclude(us => us.Skill)
        .SingleOrDefault(u => u.Id == id);

    if(result is null)
        return ResultViewModel<UserViewModel>.Error("Usuário não encontrado");

    var model = UserViewModel.FromEntity(result);

    return ResultViewModel<UserViewModel>.Success(model);
}
```
## [Seção 2]

[Descrição breve sobre o tópico abordado.]


```csharp
for (int i = 0 ; i < 10; i++)
{
    // Code to execute.
}
```
---
