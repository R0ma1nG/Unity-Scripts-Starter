package objects.json

data class JsonTranslationList(
    val items: List<JsonTranslationKeyValue>
)

data class JsonTranslationKeyValue(
    val key: String,
    val value: String
)
