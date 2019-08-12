package objects.mapper

import objects.TranslationItem
import objects.json.JsonTranslationKeyValue
import objects.json.JsonTranslationList

class JsonModelMapper {
    companion object {
        fun toJsonObject(translations: List<TranslationItem>, language: String): JsonTranslationList {
            val translationList = mutableListOf<JsonTranslationKeyValue>()
            translations.forEach {
                translationList.add(JsonTranslationKeyValue(it.key, it.translations.getOrDefault(language, "")))
            }
            return JsonTranslationList(translationList)
        }
    }
}
