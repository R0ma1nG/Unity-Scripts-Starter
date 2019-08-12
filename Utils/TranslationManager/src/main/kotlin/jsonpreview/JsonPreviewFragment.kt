package jsonpreview

import com.google.gson.GsonBuilder
import event.PreviewJsonEvent
import event.PreviewJsonRequest
import javafx.beans.property.SimpleStringProperty
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch
import tornadofx.Fragment
import tornadofx.textarea

class JsonPreviewFragment : Fragment() {
    private val gson = GsonBuilder().setPrettyPrinting().create()
    private var content = SimpleStringProperty("Loading...")

    init {
        subscribe<PreviewJsonEvent> { event ->
            content.value = gson.toJson(event.result)
        }
        GlobalScope.launch {
            delay(500)
            fire(PreviewJsonRequest)
        }
    }

    override val root = textarea(content) {
        isEditable = false
    }
}
