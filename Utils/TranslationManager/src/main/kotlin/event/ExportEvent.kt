package event

import objects.TranslationItem
import tornadofx.FXEvent

object ExportRequest : FXEvent()

data class ExportEvent(
    val data: List<TranslationItem>,
    val languages: List<String>
) : FXEvent()
