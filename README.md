# ASP.NET SignalR
**Konuşmacı:** Yiğit HACIEFENDİOĞLU

![Bilge Adam Webinar](afis.jpg)

**Tarih:** 18.10.2023

**Başlangıç - Bitiş Saati:** 20:00 - 21:00

**Link:** https://www.youtube.com/watch?v=1pzsA-n8JK8


## Faydalı Linkler
- https://dotnet.microsoft.com/en-us/apps/aspnet/signalr
- https://learn.microsoft.com/en-us/aspnet/core/signalr/introduction
- https://github.com/SignalR/SignalR
- https://learn.microsoft.com/en-us/aspnet/core/tutorials/signalr?tabs=visual-studio&view=aspnetcore-7.0 


## ASP.NET SignalR Nedir?
- ASP.NET geliştiricilerinin gerçek zamanlı web uygulaması geliştirme sürecini aşırı kolay hale getiren bir kütüphanedir.
- Server ve Client arasında çift yönlü (bi-directional) sürekli bağlantı sağlar.
- Web Sockets’i destekler.
- Yeni teknolojileri desteklemeyen eski tarayıcılarda çalışabilmesi için o tarayıcılara uygun teknikleri (polling, forever frames, vs) de ihtiyaç halinde kullanır.

## SignalR Fallback
- Web Sockets
- Server Sent Events
- Forever Frames
- Long Polling

## Kurulum
Bir ASP.NET Web Application projesi açıldıktan sonra NuGet Package Manager aracılığıyla Microsoft.AspNet.SignalR kütüphanesi yüklenir.

> Install-Package Microsoft.AspNet.SignalR

## Program.cs
```csharp
// ...

// Add services to the container.
builder.Services.AddSignalR();

// ...

app.MapHub<SohbetHub>("/sohbetHub");
app.Run();

```

## SohbetHub.cs
```csharp
using Microsoft.AspNetCore.SignalR;

namespace SignalROnCalisma.Hubs
{
    public class SohbetHub : Hub
    {
        public async Task MesajGonder(string kullanici, string mesaj)
        {
            await Clients.All.SendAsync("YeniMesaj", kullanici, mesaj);
        }
    }
}
```

## Index.cshtml
```html
<form id="frmMesaj">
    <input id="txtMesaj" type="text" placeholder="mesajınız.." required />
    <button>Gönder</button>
</form>

<ul id="mesajlar"></ul>
```

## _Layout.cshtml
```html
<script src="~/lib/microsoft/signalr/dist/browser/signalr.min.js"></script>
```

## JavaScript
```javascript
const ul = document.getElementById("mesajlar");
const txtMesaj = document.getElementById("txtMesaj");
const frmMesaj = document.getElementById("frmMesaj");
const kullanici = prompt("Kullanıcı adınız:");

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/sohbetHub")
    .build();

connection.on("YeniMesaj", function (kullanici, mesaj) {
    const li = document.createElement("li");
    li.textContent = `${kullanici}: ${mesaj}`;
    ul.prepend(li);
});

frmMesaj.onsubmit = function (e) {
    e.preventDefault();

    connection
        .invoke("MesajGonder", kullanici, txtMesaj.value)
        .catch(function (err) {
            console.log(err);
        });

    txtMesaj.value = "";
};

connection.start()
    .then(function () {
        console.log("bağlantı sağlandı");
    })
    .catch(function (err) {
        console.log("bağlantı hatası");
    });
```
