# ASP.NET-Portfolio-demo

**Beschrijving:** Een demo portfolio webapplicatie waarbij projecten weergegeven, aangemaakt, gewijzigd en verwijderd kunnen worden.
- - - -
### Gebruikte technologie ###
* ASP.NET MVC (MVVM)
* Entity framework (Code-First) met Fluent API en LINQ extenstion methods
* Migrations
* Repository pattern
* AngularJS
* Bootstrap
* SCSS
- - - -
### Gebruikte software/tools ###
* Visual Studio (2017)
* ReSharper 
* Productivity Power Tools 2017
* Web Essentials 2017
- - - -
### Code/asset beschrijving ###

#### Portfolio project ####

Bestanden/map  | Beschrijving
------------- | -------------
Content/Images  | Gebruikte afbeeldingen
Content/Styles  | SCSS bestanden
Controllers/HomeController.cs  | Weergeven van home-pagina en contact-pagina
Controllers/ProjectCategoryController.cs  | Categorieën die bij een project horen (many-to-many)
Controllers/ProjectController.cs  | Controller voor de projecten
Controllers/TagController.cs  | Tags die bij een project horen (many-to-many)
Helpers/*  | Hulp klassen
Migrations/Configuration.cs  | Om data te genereren via het "update-database" command
Models/*  | Model klassen
Repository/* | Repository klassen
ViewModels/*  | ViewModel klassen
Scripts/Project/*  | Javascript (AngularJS) voor het ophalen van many-to-many tags en categorieën op de project "create" en "update" pagina's
Views/Home/*  | Home-pagina en contact-pagina
Views/Project/*  | Project views
Views/Shared/*  | Layout views
- - - -

### Bijkomende uitleg ###

* De teksten ("lorem ipsum") zijn grootendeels gegenereerd met "http://nl.lipsum.com" om delen van de pagina-inhoud op te vullen.
* De database kan gegenereerd worden door het "update-database" command in de "Package Manager Console".
* Het aanmaken/beheer van de gebruikers, beheer van projectcategorieën en tags, code tests, vertalingen en beheer van thumbnails heb ik in het huidige project niet toegevoegd.
* Om e-mails te versturen moet "system.net/mailSettings" aangepast worden in "Portfolio/Web.config", voornamenlijk "from", "userName" en "password", standaard gebruik ik "https://mailtrap.io".