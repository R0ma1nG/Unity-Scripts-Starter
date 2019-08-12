package content

import event.*
import javafx.collections.ObservableList
import objects.TranslationItem
import objects.mapper.JsonModelMapper
import tornadofx.Controller
import tornadofx.observableListOf
import tornadofx.observableMapOf

class ContentController : Controller() {
    private var languages = listOf<String>()
    var data: ObservableList<TranslationItem> = observableListOf()
    var currentSelectedItem: TranslationItem = if (data.isNotEmpty()) {
        data[0]
    } else {
        TranslationItem(Int.MIN_VALUE, "", observableMapOf())
    }

    init {
        subscribe<ExportRequest> {
            fire(ExportEvent(data.toList(), languages))
        }

        subscribe<SaveRequest> {
            fire(SaveEvent(data.toList(), languages))
        }

        subscribe<NewTranslationItemEvent> {
            data.add(TranslationItem(data.size, "", observableMapOf()))
        }

        subscribe<RemoveTranslationItemEvent> {
            if (currentSelectedItem.id != Int.MIN_VALUE) {
                data.remove(currentSelectedItem)
                currentSelectedItem = TranslationItem(Int.MIN_VALUE, "", observableMapOf())
            }
        }

        subscribe<PreviewJsonRequest> {
            val res = languages.map {
                JsonModelMapper.toJsonObject(data, it)
            }
            fire(PreviewJsonEvent(res))
        }
    }

    fun replaceData(translations: List<TranslationItem>, newLanguages: List<String>) {
        data.removeAll(data)
        data.addAll(translations)
        languages = newLanguages
    }
}
