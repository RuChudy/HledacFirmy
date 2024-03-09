## Use Cases Project

V Clean Architecture je projekt Use Cases (nebo Application Services) relativně tenká vrstva, která obaluje doménový model.

Případy použití jsou obvykle uspořádány podle funkce. Mohou to být jednoduché operace CRUD nebo mnohem složitější činnosti.

Případy použití by neměly přímo záviset na problémech s infrastrukturou, takže je ve většině případů lze jednoduše testovat.

Případy použití jsou často seskupeny do příkazů a dotazů po CQRS.

Použití případů použití jako samostatného projektu může snížit množství logiky v projektech uživatelského rozhraní a infrastruktury.

U jednodušších projektů lze projekt Use Cases vynechat a jeho chování přesunout do projektu uživatelského rozhraní, buď jako samostatné služby nebo obslužné nástroje MediatR, nebo jednoduše vložením logiky do koncových bodů API.

### English

In Clean Architecture, the Use Cases (or Application Services) project is a relatively thin layer that wraps the domain model.

Use Cases are typically organized by feature. These may be simple CRUD operations or much more complex activities.

Use Cases should not depend directly on infrastructure concerns, making them simple to unit test in most cases.

Use Cases are often grouped into Commands and Queries, following CQRS.

Having Use Cases as a separate project can reduce the amount of logic in UI and Infrastructure projects.

For simpler projects, the Use Cases project can be omitted, and its behavior moved into the UI project, either as separate services or MediatR handlers, or by simply putting the logic into the API endpoints.

