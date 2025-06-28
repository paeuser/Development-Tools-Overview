# AgentPortal

- [AgentPortal](#agentportal)
  - [Project Setup](#project-setup)
  - [Project Guidelines and Standards](#project-guidelines-and-standards)
  - [Architecture](#architecture)
    - [Why do we need a BFF?](#why-do-we-need-a-bff)
    - [Authentication](#authentication)
    - [SEO and SMO](#seo-and-smo)
  - [Authentication](#authentication-1)
    - [Sessions and Refresh Tokens](#sessions-and-refresh-tokens)
      - [Guidelines](#guidelines)
      - [Ping Refresh settings](#ping-refresh-settings)
      - [Data Protection API](#data-protection-api)
  - [Feature Flags](#feature-flags)
    - [Adding a new Feature Flag](#adding-a-new-feature-flag)
  - [Directus](#directus)
    - [Content Structure](#content-structure)
      - [M2A (Many to Any) fields](#m2a-many-to-any-fields)
  - [API Client](#api-client)
    - [Using the client](#using-the-client)

## Project Setup

Requirements:

- [Node v18.x](https://nodejs.org/en/download/)
- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

- Clone the repository
- Connect to Assurity's NPM registry
  - Follow instructions in the "Other" tab [here](https://tfs1.assurity.local:8080/Assurity%20Projects%20Collection/AssureLinkRewrite/_packaging?_a=connect&feed=npm-packages).
    - Click "npm" first
  - Ignore the first part about the .npmrc file in the project.  That part is already done.  Skip to the "Setup Credentials" part.
  - Execute the powershell script 'LoadCertAuthority' located in the Scripts folder at the root of this repository.

- Navigate inside the Assurity.AgentPortal.Web\App folder:
  - Run `npm install`
  - Run `npm run dev`
- Run the Asp.Net Core application
- (Optional) Run the AgentPortalAPI project
- Navigate to `https://localhost:7000`

Please make sure to read through the [Project Guidelines](#project-guidelines)

You will need an existing Agent account in Ping.

I've found it is much easier to work with Vue in Visual Studio Code.  Feel free to open the App folder in VS Code and leave this project open in Visual Studio.

For Vue in Visual Studio Code ensure the following are installed as Extensions.

- [Vue - Official VS Code Extension](https://marketplace.visualstudio.com/items?itemName=Vue.volar)
- [GraphQL: Syntax Highlighting](https://marketplace.visualstudio.com/items?itemName=GraphQL.vscode-graphql-syntax) GraphQL Syntax Highlighting for template literals in TypeScript
- [ESLint](https://marketplace.visualstudio.com/items?itemName=dbaeumer.vscode-eslint) - Provides JavaScript and TypeScript linting, along with the plugins for Vue and Prettier. This is specifically for the Vue application side of things, nothing related to the core project.
  - [Setup ESLint to run on save](https://assuritylife.sharepoint.com/sites/AssurityWebProperties/SitePages/Visual-Studio-Code---AutoSave-ESLint.aspx)

Optional Extensions

- [Vuetify-vscode](https://marketplace.visualstudio.com/items?itemName=vuetifyjs.vuetify-vscode) (We are aiming to remove vuetify from the project long term)

The last one is probably optional but will make it easier to work with Vuetify.

## Project Guidelines and Standards

Hopefully these guidelines are beneficial and help us keep the code as clean and efficient as possible.

- Keep as much business logic out of the Vue app as possible.  It should really be driven by the AgentPortalAPI.
  - This will help keep the code clean and make it much easier to implement changes.
  - It will also eliminate the need for environment files for the Vue app.  Everything should be driven by either the backend or the AgentPortalAPI.
- No authentication logic in the Vue app.  The only thing that may be considered authentication logic that we can consider adding is if we get a 401 when retrieving data.  We can then redirect to the BFF's login endpoint.
- The BFF is only concerned with ensuring the Vue app can work.  It really only cares about authentication and proxying API requests on behalf of the SPA to the AgentPortalAPI, so business logic should be kept out of this project as well.
- The Vue App uses Vuetify as a component library.  Let's utilize this library as much as possible so we can quickly deliver the MVP.  We can explore replacing it with custom implementations when we get to that point.
  - <https://next.vuetifyjs.com/en/>
- Styling
  - Use BEM for CSS naming conventions.
    - Use a short prefix for a block CSS class. (Ex. .block)
    - Use the block name, then two underscores, then the element name for elements. (Ex. .block__elem)
    - Use two dashes after the element class name for modifiers. (Ex. .block__elem--mod)
    - Check out the BEM documentation for more information and examples.  <https://getbem.com/>

- The Vue app uses Prettier for code styling and ESLint for linting.  This can cause some friction if things aren't setup correctly.  This came installed out of the box with Vue 3.  If we run into issues we can look into removing one of them.
  - Run `npm run format` to automatically fix Prettier and ESLint errors.
- Please be weary when adding new NPM packages.  Let's try to keep the amount of dependencies low and only add what is explicitly required.
  - If you feel an NPM package should be added, bring it up to the team either in a pull request or via a group chat and we can discuss.

I'll be updating this regularly as more developers are onboarding to the project.  Hopefully this will cover any questions that may pop up.

## Architecture

The architecture of this project is based on what is called Backend for Frontend (or BFF). The idea is that the Asp.Net Core backend is tightly coupled to the Vue frontend. They both run on the same domain, so all api calls to the backend are just sent to `/API/SomeRoute` and we do not have to worry about Cors.

The BFF is responsible for managing authentication and proxying all other requests to the proper APIs. If we do decide to use Razor Pages for displaying Directus content, we can utilize the BFF for that as well.

### Why do we need a BFF?

The BFF handles anything that the SPA cannot be trusted with. The biggest problems this solves is:

1. How do we (securly) talk to Ping if we cannot secure a client secret?
2. How can we ensure the tokens from Ping are kept secure?
3. How can we talk to a database when the SPA is running on a user's machine?
4. How can we do all of this without allowing unauthenticated traffic into our LAN? (How WPS and Quickstart currently operate.)

The BFF is able to manage all of this. It can ensure the user is authenticated with Ping. If the user tries to get data from the BFF, the BFF will see that the browser is not authenticated because they did not send a valid cookie. The BFF can then begin the OIDC Code flow (with PKCE) and redirect the user to authenticate with Ping.

After that is complete, Ping redirects them to the BFF with the authentication code. The BFF can then send the code, client Id, and client Secret back to Ping and get OIDC tokens. The BFF then encrypts the tokens and necessary claims into an HttpOnly, Secure cookie. This ensures that the cookie is only ever sent over HTTPS and javascript running in the browser cannot access the cookie.

As far as talking to a database, the BFF will decrypt the cookie upon a request for data and grab the access token from it and send it along with the request to the AgentPortalAPI which can verify the token is actually from Ping and use the necessary claims to get the correct data from any database inside the LAN.

The BFF really gives us almost endless extensibility that we can add to our SPA because it is running on the server and we know it is secure.

### Authentication

This project uses custom middleware to ensure that requests are authenticated before serving specific routes or static files. The simplist example of this is in the Middleware folder in the `ProxyAuthGuardMiddleware`. All it is doing is checking that the user is authenticated, if they are not, it sets the status code to 401 and ends the request.

More to come on this later.

### SEO and SMO

My vision for this application is that the AgentPortal part of the site would run in Vue. This would allow us to take advantage of rich interactivity that SPAs tend to offer. I would like to use Razor Pages for the landing page, Directus content pages, and maybe some general error pages etc. I think Razor Pages are a great add-on because they are server-side rendered so we can ensure that SEO and SMO are not left out. Razor Pages can also be customized quite a bit. You can even run Vue code inside of Razor Pages!

I had a working prototype setup that serves Razor Pages and the Vue app from the same URL. The Razor Pages served as the home page, a page for viewing a list of Directus Content, and a template was setup to display each individual Directus article. I think using Razor Pages along with Vue can be extremely beneficial in ensuring our content is discovered and shareable.

For this prototype, the Vue app ran under the /app directory. If a request came in that did not match /app, the ASP.NET Core BFF would try to serve the request first, if it could not, it fellback to the Vue app. We can customize how this all works of course.

---

## Authentication

### Sessions and Refresh Tokens

The main web session is maintained with a cookie. Right now, the cookie's lifetime is driven by the Session:MaxAge variable in appsettings.json. Each time the user makes a request to the URL, either by initially navigating here or calling an API endpoint, the cookie is decrypted and validated automatically by ASP.NET Core.

In order to ensure this session does not continue after the Ping OpenId session has ended, the expires time of the access token is checked each time the cookie is validated. If the token is within 5 minutes of expiring, the refresh token is used to retrieve a new set of tokens from Ping.

#### Guidelines

The access token should be short lived. If an access token is stolen, whoever possesses that token has access to any resources it gives access to. Right now, that is just the AgentPortalApi but may expand to more resources in the future.  
Another reason for keeping the lifetime short is because if the user has their access shut off in Ping by an administrator, the cookie the ASP.NET app issues will still be valid. Using the refresh token forces the user to check in with the authorization server, Ping in this case, and if they have had access removed then no new tokens will be issued and we will know to reject this user's cookie.

The refresh token can be longer lived. Each refresh token has a one-time use. When refreshing tokens, the user is issued a new refresh token as well as an access token.  
The cookie issued from our application should have a max age setting that matches the OIDC Refresh token duration.

#### Ping Refresh settings

Ping has a few options to customize the Refresh Token flow. Administrators are able to set the Refresh Duration and the Refresh Token Rolling Duration on a per-application basis in the Ping Cloud Console.

The **Refresh Duration** setting will set the amount of time that a refresh token is valid for. For example, the user logs in to our website and gets a fresh set of tokens. They load some data and then walk away from their computer. They come back after 5 hours. The Refresh Duration is set to 4 hours. When they attempt to access the site, the refresh token process fails and the user is redirected to Ping to authenticate again.

The **Refresh Token Rolling Duration** is the amount of the that the user can keep refreshing tokens. Lets say the Refresh Duration is set to 1 day. This allows the user to access the site once every 24 hours and get a new set of tokens. They will not be forced to login as long as they are using the website within 24 hours. The Refresh Token Rolling Duration value will force the user to login if they have not logged in in the time set. So in our example, lets say the Refresh Token Rolling Duration is set to 30 days. If the user continues their practice of accessing the site daily, after 30 days of never re-authenticating with Ping, the refresh token will not be issued and the user will be forced to login to Ping again.

Right now in Ping the Refresh Token Duration for this application is set to 8 hours. The Refresh Rolling Grace Period is set to 30 days. These values are subject to change as new requirements come in.

#### Data Protection API

Asp.Net Core generates encryption keys and encrypts the authentication cookie values using these these keys.  Unfortunately, since we are deploying this application to two servers and load balancing, the encryption keys are stored in each individual machine's registry.  They cannot decrypt cookies that were issued by the other app running on the other server.

To fix this, I configured the DataProtection portion to store the keys in Redis.  Each application can generate and get keys from the shared Redis instance.  Changing the storage location of the encryption keys turns off encryption-at-rest.  There is a self-signed certificate that the Net Admins installed on both machines.  The certificate thumbprint is used to identity it.  It is used to encrypt the keys at rest so even if someone gains access to our Redis instance, they cannot decrypt the keys without the certificate.

## Feature Flags

The Microsoft Feature Management library was implemented to add feature flag support.  More on that library can be found [here](https://github.com/microsoft/FeatureManagement-Dotnet).

The feature flags are managed in Directus via a custom implementation of the IFeatureDefinitionProvider interface.  More on that interface can be found [here](https://github.com/microsoft/FeatureManagement-Dotnet#custom-feature-providers).

### Adding a new Feature Flag

1. Add a string constant to the FeatureFlags.cs file in the Web project.
1. Add the same string to the Agent Center Features collection in Directus.
    - It is important the strings match, including the casing.
    - I suggest you start in the Staging environment which drives the local environment.  Test locally to ensure it is working.
    - Agent Center Feature flag collection in Staging Directus: <https://assuritylifecms-stage.directus.app/admin/content/agent_center_features>
    - Check the box under in any environments you want the feature flag to be turned on in.
1. Use the Feature Manager class to enable or disable controllers, or get the status of the feature in an if statement
    - More examples of how to check features can be found [here](https://github.com/microsoft/FeatureManagement-Dotnet#consumption) and [here](https://github.com/microsoft/FeatureManagement-Dotnet#aspnet-core-integration)

## Directus

### Content Structure

#### M2A (Many to Any) fields

Any time you have a M2A relationship allowing the Directus user to "build" the page, please follow the existing structure used by `DynamicComponentList.vue`. The M2A field should be named "content" unless you identify that it is serving some unique purpose not covered by `DynamicComponentList` and `DynamicComponentList` either can not or should not be updated to accomodate that purpose.

## API Client

There is now an auto-generated API client including request and response contracts.  The NPM package is auto-generated when the New AssureLink API is updated with any relevant changes.  The NPM package is then published to the Assurity NPM registry and a PR is opened for the new version.

### Using the client

```
import { CommissionsAndDebtService } from "@assurity/newassurelink-client";

const response = await CommissionsAndDebtService.getPolicyDetails({
  cycleBeginDate: "01-01-2024",
  cycleEndDate: "02-01-2024",
  page: 1,
  pageSize: 10
});
```

Note: Some of the older controller endpoints have unknown as the response type.  This is an issue with the API not defining the response types and will not be an issue going forward.  Some of the old Policy Info endpoints also just pass what we send in as the query string, so the request parameters are not available in certain request types.  Again, this will have to be fixed in the future but will not be an issue with new requests.
