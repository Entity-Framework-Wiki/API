# Introduction

Ce projet a pour objectif d'illustrer la conception d'une API modulaire et adaptable � diff�rentes typologies d'applications. L'architecture mise en �uvre est con�ue pour automatiser plusieurs aspects du d�veloppement, notamment l'acc�s aux donn�es, l'impl�mentation des normes REST et l'authentification.

### Technologies Utilis�es

* **Entity Framework Core :** Fournit une couche d'abstraction pour l'acc�s aux donn�es tout en permettant d'ex�cuter des requ�tes SQL pures pour optimiser les performances. Il s'int�gre parfaitement avec OData, permettant une configuration simplifi�e.

* **OData :** Automatisation des normes REST, permettant de r�cup�rer uniquement les donn�es n�cessaires � l'application, de mani�re similaire � GraphQL.

* **Auth0 :** Gestion des identit�s et des acc�s (IAM), avec int�gration d'OAuth0 pour la s�curisation des endpoints.

Cette architecture permet de structurer les modules de mani�re � maintenir une base de code �volutive et maintenable, tout en respectant les principes de s�paration des responsabilit�s et de modularit�.

 
## Initialisation de la Base de Donn�es

Allez directement sur la section "Lancement du projet" si vous souhaitez uniquement ex�cuter le projet. Cette section sert principalement de m�mo pour rappeler les �tapes cl�s de l'initialisation de la base de donn�es.

L'initialisation de la base de donn�es est une �tape cruciale dans le processus de d�veloppement d'une API. Elle permet de structurer les donn�es et de pr�parer l'environnement pour les op�rations CRUD (Create, Read, Update, Delete). Dans ce projet, nous adoptons l'approche **Database First**, particuli�rement efficace lorsque nous devons connecter notre application � une base de donn�es d�j� existante ou lorsque la structure des donn�es est d�j� bien d�finie.

Allez directement sur la section "Lancement du Projet". Ceci est un m�mo plus qu'autre chose.

### Pourquoi Database First (optionnel) ?

* **Simplicit� :** L'approche Database First permet de g�n�rer automatiquement les mod�les d'entit�s � partir de la base de donn�es existante.
* **Flexibilit� :** Id�ale pour les projets o� le sch�ma de base de donn�es est susceptible de changer.
* **Rapidit� :** Permet de rapidement g�n�rer le contexte et les classes d'entit�s sans avoir � configurer manuellement le mapping Fluent API.

### Structure de la Base de Donn�es (optionnel)

Dans notre projet, nous avons d�fini une structure de base de donn�es SQL Server comprenant plusieurs modules : Produits, Commandes, Facturation, Rapports et Statistiques.

Voici le script SQL permettant de cr�er cette base de donn�es :

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

Pour configurer l'environnement et g�n�rer les entit�s, les packages suivants doivent �tre install�s :

* `Microsoft.EntityFrameworkCore.Design`
* `Microsoft.EntityFrameworkCore.Tools`
* `Microsoft.EntityFrameworkCore.SqlServer`

Commandes d'installation :

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

### G�n�ration des Entit�s et du Contexte

Pour g�n�rer les entit�s et le contexte de base de donn�es, ex�cutez la commande suivante :

```bash
dotnet ef dbcontext scaffold "Server=localhost;Database=GestionCommerce;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Entities --context-dir ./
```

Pour g�n�rer des modules sp�cifiques :

```bash
dotnet ef dbcontext scaffold "Server=localhost;Database=GestionCommerce;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --table Produit -o Modules/Products/DAL/Produit --context GestionCommerceContext --no-onconfiguring
```

### Cr�ation de la Premi�re Migration

Pour cr�er la premi�re migration :

```bash
dotnet ef migrations add InitialDatabase
```

Ensuite, pour appliquer la migration :

```bash
dotnet ef database update
```

### Synchronisation Manuelle de la Migration

Ins�rez manuellement la migration dans le journal des migrations :

```sql
INSERT INTO __EFMigrationsHistory(MigrationId, ProductVersion) VALUES ('20250509065839_InitialDatabase', 9.0)
```

### Lancement du Projet

Pour lancer le projet et initialiser la base de donn�es, ex�cutez la commande suivante :

```bash
dotnet ef database update
```

Cette commande appliquera toutes les migrations et cr�era la base de donn�es avec la structure d�finie dans le contexte.

En cas de modifications ou d'ajouts dans le sch�ma de base de donn�es, n'oubliez pas de cr�er une nouvelle migration :

```bash
dotnet ef migrations add NouvelleMigration
```

Puis, appliquez-la :

```bash
dotnet ef database update
```

Le projet est maintenant pr�t � �tre utilis� et � communiquer avec la base de donn�es.



## Configuration et Automatisation des Normes REST

Pour garantir une conformit� stricte avec les normes REST tout en automatisant les processus de requ�tes, le projet utilise **OData** associ� � **Entity Framework Core**. Cette combinaison permet de simplifier les op�rations CRUD tout en optimisant les requ�tes pour r�cup�rer uniquement les donn�es n�cessaires.

### Packages Requis

Pour configurer OData dans le projet, les packages suivants doivent �tre install�s :

```bash
 dotnet add package Microsoft.AspNetCore.OData
 dotnet add package Microsoft.OData.ModelBuilder
```

> **Note :** � l'heure actuelle, OData ne prend pas en charge les APIs minimales. Par cons�quent, il est n�cessaire d'utiliser l'approche traditionnelle bas�e sur les controllers.

---

### Configuration des Controllers OData

Pour chaque entit�, un contr�leur OData est cr��. Voici un exemple de configuration d'un contr�leur pour l'entit� `Produit` :

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

L'utilisation des EDM permet de contr�ler les donn�es expos�es par les endpoints OData. Cela permet de limiter l'exposition des propri�t�s et de structurer les relations entre entit�s.

Exemple de d�finition d'un EDM :

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

### Limitation des Donn�es Expos�es

Pour renforcer la s�curit� et limiter l'exposition des donn�es, il est possible de n'utiliser que des DTOs (Data Transfer Objects) dans les EDM. Cela permet de masquer la structure interne de la base de donn�es tout en contr�lant les propri�t�s expos�es.

De plus, pour des raisons de s�curit� ou de lisibilit�, il est �galement possible de renommer des propri�t�s expos�es tout en conservant le nom d'origine des colonnes en base de donn�es. Par exemple :

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

Dans cet exemple, la propri�t� `Nom` expos�e dans l'API fait r�f�rence � la colonne `NomProduit` dans la base de donn�es, permettant ainsi de masquer la structure interne sans compromettre le mapping des donn�es.

---

### Ex�cution des Requ�tes OData

Les requ�tes OData permettent d'ex�cuter des filtres complexes tout en limitant le volume de donn�es retourn�. Par exemple :

* Obtenir les produits avec uniquement les propri�t�s `Nom` et `ProductId` :

```
GET https://localhost:7192/odata/Produit?$select=Nom,ProductId
```

* Obtenir les produits et leurs lignes de commande associ�es :

```
GET https://localhost:7192/odata/Produit?$expand=LigneCommandes
```

Cette structure permet une gestion centralis�e des acc�s aux donn�es, tout en automatisant le respect des normes REST via OData.


### Limitations d'OData

OData est un protocole puissant pour interroger et manipuler des donn�es via des API RESTful. Cependant, il pr�sente certaines limitations notables � prendre en compte lors de son utilisation :

1. **G�n�ration de clients OData :**

   * Actuellement, le seul client officiellement pris en charge pour g�n�rer des requ�tes OData est le package `Microsoft.OData.Client` pour .NET. Ce package est activement maintenu par les contributeurs officiels d'OData et offre une abstraction puissante pour interroger des donn�es de mani�re typ�e via LINQ.
   * Pour d'autres langages, il n'existe pas de package standardis� officiellement maintenu. Cependant, il existe des biblioth�ques communautaires et open source pour des langages comme Java, JavaScript ou Python, mais leur niveau de support peut varier.

2. **Conversion des metadata en OpenAPI :**

   * OData expose les m�tadonn�es de ses services sous forme d'un document CSDL (Common Schema Definition Language), mais il n'existe pas de moyen standardis� ou officiellement soutenu pour convertir ces m�tadonn�es en OpenAPI.
   * Bien qu'il existe des outils open source comme `odata-openapi`, ceux-ci ne sont pas officiellement pris en charge par le consortium OData et peuvent pr�senter des limitations ou des diff�rences d'impl�mentation.

3. **Couche d'abstraction pour l'interrogation des donn�es :**

   * Le client OData pour .NET offre une couche d'abstraction puissante permettant de manipuler des donn�es via LINQ, ce qui simplifie les requ�tes complexes.

#### Exemple d'utilisation d'un client OData en .NET :

```csharp
var serviceRoot = "https://services.odata.org/V4/TripPinServiceRW/";
var context = new DefaultContainer(new Uri(serviceRoot));

// Exemple : r�cup�rer les personnes dont le pr�nom commence par "S",
// inclure leurs amis (expand), et ne s�lectionner que le pr�nom et la liste des amis (select)
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

En r�sum�, bien qu'OData fournisse une structure puissante et un mod�le standardis� pour interroger des API RESTful, les limitations li�es � la g�n�ration de clients et � la conversion vers OpenAPI doivent �tre prises en compte lors de la planification d'une architecture bas�e sur OData.


## Authentification

L'authentification est un aspect essentiel pour s�curiser l'acc�s aux ressources de l'API. Dans ce projet, nous avons choisi d'utiliser **Auth0**, un fournisseur de gestion des identit�s (IAM) qui propose une int�gration simple avec OAuth 2.0 et JWT (JSON Web Tokens).

### Pourquoi Auth0 ?

* **Facilit� de mise en �uvre :** Auth0 propose une documentation compl�te et des exemples pr�ts � l'emploi pour impl�menter rapidement une authentification s�curis�e.
* **Modularit� :** Auth0 permet de g�rer plusieurs types de connexions (e-mail/mot de passe, r�seaux sociaux, SSO, etc.).
* **Scalabilit� :** Auth0 est con�u pour s'int�grer � des applications de toutes tailles.
* **Conformit� :** Auth0 respecte les normes de s�curit� telles que OAuth 2.0, OpenID Connect et JWT.
* **Limitation :** Auth0 est un service commercial am�ricain. Pour des applications n�cessitant une infrastructure 100 % locale ou un h�bergement en dehors des �tats-Unis, des alternatives telles que **Keycloak** ou **IdentityServer** peuvent �tre envisag�es.

### Mise en place de l'authentification avec Auth0

Pour impl�menter une authentification basique avec Auth0, suivez ces �tapes :

1. **Cr�ation d'une API sur Auth0 :**

   * Connectez-vous � votre tableau de bord Auth0.
   * Acc�dez � **APIs > Create API**.
   * Renseignez le nom et l'identifiant de l'API. Cet identifiant est unique et immuable. Par convention, il est conseill� d'utiliser l'URL de l'API (par exemple, `https://gestion-commerce.com`).

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

// Obligatoire m�me si utilisation uniquememnt pour l'autthenttification
app.UseAuthorization();
```

3. **Obtention d'un access token :**

Pour obtenir un token d'acc�s, ex�cutez la commande suivante :

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

Le token obtenu doit �tre inclus dans chaque requ�te vers les endpoints s�curis�s :

```bash
curl -X GET "https://localhost:7192/odata/Produit?expand=LigneCommandes" \
  -H "Authorization: Bearer <access_token>"
```


// Par la suite cr�� une section sur les authorisation. 






