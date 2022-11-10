# Servidor per replicar el taulell en altres pantalles

El servidor s'executa en el port 44080. Per poder tenir accés al port, cal executar l'aplicació com administrador 
o reservar el port a Access Control Lists

## Afegir la reserva a urlacl

```
netsh http add urlacl url=http://*:44080/ user=todos
```

