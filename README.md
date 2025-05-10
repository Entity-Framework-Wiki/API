# Introduction

Ce projet a pour objectif d'illustrer la conception d'une API modulaire et adaptable à différentes typologies d'applications. L'architecture mise en œuvre est conçue pour automatiser plusieurs aspects du développement, notamment l'accès aux données, l'implémentation des normes REST et l'authentification.

### Technologies Utilisées

* **Entity Framework Core :** Fournit une couche d'abstraction pour l'accès aux données tout en permettant d'exécuter des requêtes SQL pures pour optimiser les performances. Il s'intègre parfaitement avec OData, permettant une configuration simplifiée.

* **OData :** Automatisation des normes REST, permettant de récupérer uniquement les données nécessaires à l'application, de manière similaire à GraphQL.

* **Auth0 :** Gestion des identités et des accès (IAM), avec intégration d'OAuth0 pour la sécurisation des endpoints.

Cette architecture permet de structurer les modules de manière à maintenir une base de code évolutive et maintenable, tout en respectant les principes de séparation des responsabilités et de modularité.

 
## Initialisation de la Base de Données

Allez directement sur la section "Lancement du projet" si vous souhaitez uniquement exécuter le projet. Cette section sert principalement de mémo pour rappeler les étapes clés de l'initialisation de la base de données.

L'initialisation de la base de données est une étape cruciale dans le processus de développement d'une API. Elle permet de structurer les données et de préparer l'environnement pour les opérations CRUD (Create, Read, Update, Delete). Dans ce projet, nous adoptons l'approche **Database First**, particulièrement efficace lorsque nous devons connecter notre application à une base de données déjà existante ou lorsque la structure des données est déjà bien définie.

Allez directement sur la section "Lancement du Projet". Ceci est un mémo plus qu'autre chose.

### Pourquoi Database First (optionnel) ?

* **Simplicité :** L'approche Database First permet de générer automatiquement les modèles d'entités à partir de la base de données existante.
* **Flexibilité :** Idéale pour les projets où le schéma de base de données est susceptible de changer.
* **Rapidité :** Permet de rapidement générer le contexte et les classes d'entités sans avoir à configurer manuellement le mapping Fluent API.

### Structure de la Base de Données (optionnel)

Dans notre projet, nous avons défini une structure de base de données SQL Server comprenant plusieurs modules : Produits, Commandes, Facturation, Rapports et Statistiques.

Voici le script SQL permettant de créer cette base de données :

```sql
CREATE DATABASE GestionCommerce;
GO

USE GestionCommerce;
GO

-- Module Produits
CREATE TABLE Produit (
    product_id INT PRIMARY KEY,
    nom NVARCHAR(100),
    categorie_id INT,
    marque_id INT,
    prix DECIMAL(10, 2)
);

CREATE TABLE Categorie (
    categorie_id INT PRIMARY KEY,
    nom NVARCHAR(100)
);

CREATE TABLE Marque (
    marque_id INT PRIMARY KEY,
    nom NVARCHAR(100)
);

CREATE TABLE Inventaire (
    inventory_id INT PRIMARY KEY,
    product_id INT FOREIGN KEY REFERENCES Produit(product_id),
    stock INT,
    fournisseur_id INT
);

CREATE TABLE Fournisseur (
    fournisseur_id INT PRIMARY KEY,
    nom NVARCHAR(100),
    contact NVARCHAR(100)
);

-- Module Commandes
CREATE TABLE Commande (
    order_id INT PRIMARY KEY,
    date_commande DATETIME,
    statut NVARCHAR(50)
);

CREATE TABLE LigneCommande (
    ligne_id INT PRIMARY KEY,
    order_id INT FOREIGN KEY REFERENCES Commande(order_id),
    product_id INT FOREIGN KEY REFERENCES Produit(product_id),
    quantite INT,
    prix_unitaire DECIMAL(10, 2)
);

-- Module Facturation
CREATE TABLE Facture (
    invoice_id INT PRIMARY KEY,
    order_id INT FOREIGN KEY REFERENCES Commande(order_id),
    montant_total DECIMAL(10, 2),
    date_facture DATETIME
);

CREATE TABLE Paiement (
    payment_id INT PRIMARY KEY,
    invoice_id INT FOREIGN KEY REFERENCES Facture(invoice_id),
    montant DECIMAL(10, 2),
    date_paiement DATETIME
);

-- Module Rapports et Statistiques
CREATE TABLE Rapport (
    report_id INT PRIMARY KEY,
    type NVARCHAR(50),
    date_creation DATETIME,
    contenu NVARCHAR(MAX)
);
```

### Packages Requis

Pour configurer l'environnement et générer les entités, les packages suivants doivent être installés :

* `Microsoft.EntityFrameworkCore.Design`
* `Microsoft.EntityFrameworkCore.Tools`
* `Microsoft.EntityFrameworkCore.SqlServer`

Commandes d'installation :

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

### Génération des Entités et du Contexte

Pour générer les entités et le contexte de base de données, exécutez la commande suivante :

```bash
dotnet ef dbcontext scaffold "Server=localhost;Database=GestionCommerce;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Entities --context-dir ./
```

Pour générer des modules spécifiques :

```bash
dotnet ef dbcontext scaffold "Server=localhost;Database=GestionCommerce;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --table Produit -o Modules/Products/DAL/Produit --context GestionCommerceContext --no-onconfiguring
```

### Création de la Première Migration

Pour créer la première migration :

```bash
dotnet ef migrations add InitialDatabase
```

Ensuite, pour appliquer la migration :

```bash
dotnet ef database update
```

### Synchronisation Manuelle de la Migration

Insérez manuellement la migration dans le journal des migrations :

```sql
INSERT INTO __EFMigrationsHistory(MigrationId, ProductVersion) VALUES ('20250509065839_InitialDatabase', 9.0)
```

### Lancement du Projet

Pour lancer le projet et initialiser la base de données, exécutez la commande suivante :

```bash
dotnet ef database update
```

Cette commande appliquera toutes les migrations et créera la base de données avec la structure définie dans le contexte.

En cas de modifications ou d'ajouts dans le schéma de base de données, n'oubliez pas de créer une nouvelle migration :

```bash
dotnet ef migrations add NouvelleMigration
```

Puis, appliquez-la :

```bash
dotnet ef database update
```

Le projet est maintenant prêt à être utilisé et à communiquer avec la base de données.



## Configuration et Automatisation des Normes REST

Pour garantir une conformité stricte avec les normes REST tout en automatisant les processus de requêtes, le projet utilise **OData** associé à **Entity Framework Core**. Cette combinaison permet de simplifier les opérations CRUD tout en optimisant les requêtes pour récupérer uniquement les données nécessaires.

### Packages Requis

Pour configurer OData dans le projet, les packages suivants doivent être installés :

```bash
 dotnet add package Microsoft.AspNetCore.OData
 dotnet add package Microsoft.OData.ModelBuilder
```

> **Note :** À l'heure actuelle, OData ne prend pas en charge les APIs minimales. Par conséquent, il est nécessaire d'utiliser l'approche traditionnelle basée sur les controllers.

---

### Configuration des Controllers OData

Pour chaque entité, un contrôleur OData est créé. Voici un exemple de configuration d'un contrôleur pour l'entité `Produit` :

```csharp
[Route("api/[controller]")]
public class ProduitController(GestionCommerceContext context) : ODataController
{
    private readonly GestionCommerceContext _context = context;

    [Route("odata/[controller]")]
    [HttpGet]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Get(ODataQueryOptions<Produit> options)
    {
        IQueryable<Produit> query = _context.Produits;

        var result = options.ApplyTo(query);


        return this.Ok(result);
    }
}
```

---

### Gestion des EDM (Entity Data Models)

L'utilisation des EDM permet de contrôler les données exposées par les endpoints OData. Cela permet de limiter l'exposition des propriétés et de structurer les relations entre entités.

Exemple de définition d'un EDM :

```csharp
using Microsoft.OData.ModelBuilder;
using GestionCommerce.Entities;

public static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    var produits = builder.EntitySet<Produit>("Produits");
    produits.EntityType.HasKey(p => p.ProductId);
    produits.EntityType.Property(p => p.Nom);
    produits.EntityType.HasMany(p => p.LigneCommandes);

    return builder.GetEdmModel();
}
```

---

### Limitation des Données Exposées

Pour renforcer la sécurité et limiter l'exposition des données, il est possible de n'utiliser que des DTOs (Data Transfer Objects) dans les EDM. Cela permet de masquer la structure interne de la base de données tout en contrôlant les propriétés exposées.

De plus, pour des raisons de sécurité ou de lisibilité, il est également possible de renommer des propriétés exposées tout en conservant le nom d'origine des colonnes en base de données. Par exemple :

```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ProduitDTO
{
    [Key]
    public int Id { get; set; }

    [Column("NomProduit")]
    public string Nom { get; set; }
}
```

Dans cet exemple, la propriété `Nom` exposée dans l'API fait référence à la colonne `NomProduit` dans la base de données, permettant ainsi de masquer la structure interne sans compromettre le mapping des données.

---

### Exécution des Requêtes OData

Les requêtes OData permettent d'exécuter des filtres complexes tout en limitant le volume de données retourné. Par exemple :

* Obtenir les produits avec uniquement les propriétés `Nom` et `ProductId` :

```
GET https://localhost:7192/odata/Produit?$select=Nom,ProductId
```

* Obtenir les produits et leurs lignes de commande associées :

```
GET https://localhost:7192/odata/Produit?$expand=LigneCommandes
```

Cette structure permet une gestion centralisée des accès aux données, tout en automatisant le respect des normes REST via OData.


### Limitations d'OData

OData est un protocole puissant pour interroger et manipuler des données via des API RESTful. Cependant, il présente certaines limitations notables à prendre en compte lors de son utilisation :

1. **Génération de clients OData :**

   * Actuellement, le seul client officiellement pris en charge pour générer des requêtes OData est le package `Microsoft.OData.Client` pour .NET. Ce package est activement maintenu par les contributeurs officiels d'OData et offre une abstraction puissante pour interroger des données de manière typée via LINQ.
   * Pour d'autres langages, il n'existe pas de package standardisé officiellement maintenu. Cependant, il existe des bibliothèques communautaires et open source pour des langages comme Java, JavaScript ou Python, mais leur niveau de support peut varier.

2. **Conversion des metadata en OpenAPI :**

   * OData expose les métadonnées de ses services sous forme d'un document CSDL (Common Schema Definition Language), mais il n'existe pas de moyen standardisé ou officiellement soutenu pour convertir ces métadonnées en OpenAPI.
   * Bien qu'il existe des outils open source comme `odata-openapi`, ceux-ci ne sont pas officiellement pris en charge par le consortium OData et peuvent présenter des limitations ou des différences d'implémentation.

3. **Couche d'abstraction pour l'interrogation des données :**

   * Le client OData pour .NET offre une couche d'abstraction puissante permettant de manipuler des données via LINQ, ce qui simplifie les requêtes complexes.

#### Exemple d'utilisation d'un client OData en .NET :

```csharp
var serviceRoot = "https://services.odata.org/V4/TripPinServiceRW/";
var context = new DefaultContainer(new Uri(serviceRoot));

// Exemple : récupérer les personnes dont le prénom commence par "S",
// inclure leurs amis (expand), et ne sélectionner que le prénom et la liste des amis (select)
var query = context.People
    .Where(p => p.FirstName.StartsWith("S"))
    .Select(p => new
    {
        p.FirstName,
        p.Friends
    })
    .Expand(p => p.Friends);

var people = await query.ExecuteAsync();
```

En résumé, bien qu'OData fournisse une structure puissante et un modèle standardisé pour interroger des API RESTful, les limitations liées à la génération de clients et à la conversion vers OpenAPI doivent être prises en compte lors de la planification d'une architecture basée sur OData.


## Authentification

L'authentification est un aspect essentiel pour sécuriser l'accès aux ressources de l'API. Dans ce projet, nous avons choisi d'utiliser **Auth0**, un fournisseur de gestion des identités (IAM) qui propose une intégration simple avec OAuth 2.0 et JWT (JSON Web Tokens).

### Pourquoi Auth0 ?

* **Facilité de mise en œuvre :** Auth0 propose une documentation complète et des exemples prêts à l'emploi pour implémenter rapidement une authentification sécurisée.
* **Modularité :** Auth0 permet de gérer plusieurs types de connexions (e-mail/mot de passe, réseaux sociaux, SSO, etc.).
* **Scalabilité :** Auth0 est conçu pour s'intégrer à des applications de toutes tailles.
* **Conformité :** Auth0 respecte les normes de sécurité telles que OAuth 2.0, OpenID Connect et JWT.
* **Limitation :** Auth0 est un service commercial américain. Pour des applications nécessitant une infrastructure 100 % locale ou un hébergement en dehors des États-Unis, des alternatives telles que **Keycloak** ou **IdentityServer** peuvent être envisagées.

### Mise en place de l'authentification avec Auth0

Pour implémenter une authentification basique avec Auth0, suivez ces étapes :

1. **Création d'une API sur Auth0 :**

   * Connectez-vous à votre tableau de bord Auth0.
   * Accédez à **APIs > Create API**.
   * Renseignez le nom et l'identifiant de l'API. Cet identifiant est unique et immuable. Par convention, il est conseillé d'utiliser l'URL de l'API (par exemple, `https://gestion-commerce.com`).

2. **Ajout du middleware d'authentification dans le projet :**

Installez le package suivant :

```bash
 dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

Modifiez le fichier `Program.cs` ou `Startup.cs` pour configurer l'authentification :

```csharp
services.AddAuthentication(options =>
   {
       options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
       options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
   }).AddJwtBearer(options =>
   {
       options.Authority = "https://dev-6s6s0f4wpurx7gmw.eu.auth0.com/";
       options.Audience = "https://gestion-commerce.com";
   });
```

Ajoutez ensuite le middleware d'authentification et d'autorisation :

```csharp
app.UseAuthentication();

// Obligatoire même si utilisation uniquememnt pour l'autthenttification
app.UseAuthorization();
```

3. **Obtention d'un access token :**

Pour obtenir un token d'accès, exécutez la commande suivante :

```bash
curl --request POST \
  --url https://dev-6s6s0f4wpurx7gmw.eu.auth0.com/oauth/token \
  --header 'content-type: application/json' \
  --data '{
    "client_id": "votre_client_id",
    "client_secret": "votre_client_secret",
    "audience": "https://gestion-commerce.com",
    "grant_type": "client_credentials"
  }'
```

Le token obtenu doit être inclus dans chaque requête vers les endpoints sécurisés :

```bash
curl -X GET "https://localhost:7192/odata/Produit?expand=LigneCommandes" \
  -H "Authorization: Bearer <access_token>"
```



// Par la suite créé une section sur les authorisation. 


## Considérations

Dans le cadre de la structuration de l'API, il est essentiel de bien réfléchir à l'architecture des contrôleurs et des agrégats. En effet, le choix de l'agrégation forte ou faible détermine la manière dont les ressources sont organisées, exposées et gérées au sein de l'API.

### 1. Agrégation Forte vs Faible

* **Agrégation Forte :**

  * Une agrégation forte implique que la ressource est strictement liée à un agrégat principal. Les actions sur cette ressource doivent impérativement passer par l'agrégat auquel elle est associée. Les contrôleurs ne sont pas créés indépendamment pour ces ressources mais sont intégrés au contrôleur principal de l'agrégat.
  * Exemple : Si une commande (`order`) possède des lignes de commande (`lines`), les lignes de commande seront toujours manipulées à travers le contrôleur des commandes.
  * Les routes pour une agrégation forte peuvent suivre la structure suivante :

    * `POST /orders/{orderId}/lines`
    * `PUT /orders/{orderId}/lines/{lineId}`
    * `DELETE /orders/{orderId}/lines/{lineId}`

* **Agrégation Faible :**

  * Une agrégation faible signifie que la ressource peut être gérée indépendamment, bien qu'elle soit rattachée logiquement à un agrégat principal. Les contrôleurs peuvent être créés séparément pour ces ressources.
  * Exemple : Un commentaire (`comment`) peut être attaché à un article (`article`), mais il peut également être modifié ou supprimé sans passer par le contrôleur d'articles.
  * Les routes peuvent être :

    * `POST /comments`
    * `PUT /comments/{commentId}`

### 2. Gestion des Relations et Limitation des Niveaux de Routage

Pour conserver la lisibilité et la simplicité des routes, il est recommandé de ne pas dépasser trois niveaux de profondeur :

* ✅ `PUT /orders/{orderId}/lines/{lineId}`
* ❌ `PUT /orders/{orderId}/lines/{lineId}/taxes/{taxId}`

Pour les niveaux plus profonds, il est préférable d'utiliser des vues dénormalisées ou des endpoints spécialisés :

* `PUT /orders/{orderId}/lines/{lineId}/update-taxes?tax_id=1`

Pour les opérations en lecture seule, les vues dénormalisées (orderLines) permettent d'accéder aux informations sans parcourir plusieurs niveaux d'agrégats. Par exemple :

* `GET /orderLines/{lineId}/taxes`

Cela permet d'obtenir toutes les taxes associées à une ligne de commande de manière optimisée et sans passer par plusieurs niveaux d'URL.

Cela permet d'améliorer les performances et d'éviter des requêtes complexes avec de multiples jointures.



### 3. Identification des Modules et Décomposition

* Lorsqu'une ressource est modifiée fréquemment ou requiert des règles métier spécifiques, il est envisageable de la transformer en module distinct.
* Un module peut contenir plusieurs agrégats. Par exemple, le module de `Facturation` peut inclure `Factures`, `Paiements` et `Remboursements`, chacun étant un agrégat distinct mais logiquement groupé sous le même module.

### 4. Conclusion

La structuration des agrégats et des modules doit être pensée en amont pour garantir la cohérence des données, la simplicité des routes et l'évolutivité de l'API. Un contrôle strict des agrégations fortes et faibles permet de conserver un design cohérent tout en limitant le risque d'incohérences transactionnelles.







// 

Expliqué également que cela peut avoir un vraie impact sur l'accés au données un State Management donc
dans un store. Et donc un store c'est pareille au niveau des routes, c'est "mappé". Mais c'est un sujet auquel
j'ai pas assez appprofondi et fait de recherche. 




// Faire des recherche sur ce concept
Les règles d'invariance
Recherche sur les consistance transactionnels

















