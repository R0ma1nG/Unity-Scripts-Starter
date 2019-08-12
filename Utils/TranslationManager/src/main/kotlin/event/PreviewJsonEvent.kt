package event

import objects.json.JsonTranslationList
import tornadofx.FXEvent

object PreviewJsonRequest: FXEvent()

data class PreviewJsonEvent(
    val result: List<JsonTranslationList>
): FXEvent()
