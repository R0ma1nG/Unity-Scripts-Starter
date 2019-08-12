package event

import objects.TranslationItem
import tornadofx.FXEvent

object SaveRequest : FXEvent()

data class SaveEvent(
    val data: List<TranslationItem>,
    val languages: List<String>
) : FXEvent()
