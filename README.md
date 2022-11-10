# QuinaNadal

Aplicació per mostrar el taulell durant una Quina de Nadal.

## Mode simple

Es mostra el taulell i es van marcant las caselles a mesura que surten les boles.

Per desmarcar una casella es pot tornar a clickar o si es vol desmarcar tot el taulell es pot fer amb __CTRL-X__

## Mode Client - Servidor

Si el volen tenir mes pantalles distribuides per la sala es pot fer utlitzant un servidor intermediari.


En aquest mode els organitzadors marcaran les caselles en l'aplicació "principal" i es replicarà el taulell en totes les aplicacions "clients"

### Servidor :

Per sincronitzar totes les apliacacións, abans de res s'ha de llençar una aplicació que faci de servidor.

Aquest servidor s'executa de forma autonoma en un dels equips (normalment en el mateix equip on es posarà l'aplicació principal) i sincronitza els taulells.

### Principal :

Es configura una aplicació com a principal i s'activa la sincronitzió amb el servidor. 

Tot els canvis que es facin en aquesta aplicació son els s'enviaran als clients.

### Clients :

Es configuren tantes aplicacions necesaries com a clients i aniran rebent la informació del taulell des del servidor.

## Configuració
La configuració es modifica dins el fitxer QuinaNadal.Config.

__Titols__

Des de la configuració es pot definir el titol que apareix damunt del taulell.

```xml
<setting name="Titol" serializeAs="String">
    <value>QUINA NADAL 2022</value>
</setting>
<setting name="Entitat" serializeAs="String">
    <value>LA MEVA ENTITAT</value>
</setting>
```

__Connexió amb el servidor:__

Tant per l'aplciació principal com pels clients, s'ha d'activar la sincronització i especificar en quin equip es troba el servidor.


```xml
<setting name="ServerUrl" serializeAs="String">
  <value>http://{ip.del.servidor}:44080</value>
</setting>

<setting name="ServerSync" serializeAs="String">
  <value>True</value>
</setting>
```

__Aplicació principal o client__ : 

Per definir l'aplicació principal cal posar el ModeServer a __true__ . Per les aplicacions clients el deixarem a __false__

```xml
<setting name="ModeServer" serializeAs="String">
  <value>True</value>
</setting>
```

__Refresc del taulell__ :

Aquest valor ens indica cada quans ms es refrescarà el taulell.

```xml
<setting name="RefreshRate" serializeAs="String">
  <value>300</value>
</setting>
```

__Mode demo__

Per poder fer proves, es pot activar el mode demo en l'aplicació principal i s'aniran activant caselles de forma aleatoria. Aquesta opció es útil per comprovar com els canvis es van replicant en les aplicacions client.

```xml
<setting name="RunDemo" serializeAs="String">
    <value>False</value>
</setting>
```