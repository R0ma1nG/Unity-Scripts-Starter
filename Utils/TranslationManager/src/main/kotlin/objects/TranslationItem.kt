package objects

import javafx.beans.property.SimpleIntegerProperty
import javafx.beans.property.SimpleStringProperty
import javafx.collections.ObservableMap
import tornadofx.*

class TranslationItem(
    id: Int,
    key: String,
    val translations: ObservableMap<String, String>
) {
    val idProperty = SimpleIntegerProperty(this, "id", id)
    var id: Int by idProperty

    val keyProperty = SimpleStringProperty(this, "key", key)
    var key: String by keyProperty

    override fun toString(): String {
        return "TranslationItem: { id: $id, key: $key, translations: $translations }"
    }

    fun checkKeyUnicity(items: List<TranslationItem>): Boolean {
        if (items.filter { it.key == key }.size > 1) return false
        return true
    }
}
