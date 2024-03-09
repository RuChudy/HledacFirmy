## Core (Domain Model) Project

V čisté architektuře by se ústřední pozornost měla zaměřit na entity a obchodní pravidla.

V Domain-Driven Design je to model domény.

Tento projekt by měl obsahovat všechny vaše entity, objekty hodnot a obchodní logiku.

Entity, které spolu souvisí a měly by se společně měnit, by měly být seskupeny do agregátu.

Subjekty by měly využít zapouzdření a měly by minimalizovat veřejné nastavovače.

Entity mohou využívat události domény ke sdělování změn do jiných částí systému.

Entity mohou definovat specifikace, které lze použít k dotazování na ně.

Pro měnitelný přístup by k entitám měl být přístup prostřednictvím rozhraní úložiště.

Dotazy ad hoc pouze pro čtení mohou používat samostatné Dotazovací služby, které nepoužívají model domény.

### English

In Clean Architecture, the central focus should be on Entities and business rules.

In Domain-Driven Design, this is the Domain Model.

This project should contain all of your Entities, Value Objects, and business logic.

Entities that are related and should change together should be grouped into an Aggregate.

Entities should leverage encapsulation and should minimize public setters.

Entities can leverage Domain Events to communicate changes to other parts of the system.

Entities can define Specifications that can be used to query for them.

For mutable access, Entities should be accessed through a Repository interface.

Read-only ad hoc queries can use separate Query Services that don't use the Domain Model.
