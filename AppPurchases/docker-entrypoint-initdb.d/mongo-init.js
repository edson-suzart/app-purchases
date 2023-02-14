onn = new Mongo();
db = conn.getDB("products");
db.apps.insertMany([
    {
        "NameApp": "Youtube",
        "PriceApp": 10,
        "DescriptionApp": "Aplicativo para assistir vídeos."
    },
    {
        "NameApp": "Whatsapp",
        "PriceApp": 100,
        "DescriptionApp": "Aplicativo para conversar online."
    },
    {
        "NameApp": "Tinder",
        "PriceApp": 50,
        "DescriptionApp": "Aplicativo para encontros."
    }
])