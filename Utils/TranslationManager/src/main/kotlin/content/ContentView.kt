package content

import event.*
import javafx.geometry.Pos
import javafx.scene.control.Label
import javafx.scene.control.TableView
import javafx.scene.control.TextField
import javafx.scene.paint.Color
import javafx.stage.Modality
import javafx.stage.StageStyle
import jsonpreview.JsonPreviewFragment
import objects.TranslationItem
import tornadofx.*

class ContentView : View() {
    private val contentController by inject<ContentController>()

    // UI Components
    private var tableView: TableView<TranslationItem>? = null
    private var formFieldset: Fieldset? = null
    private var formKey: Label? = null
    private var formTranslations: MutableMap<String, TextField> = mutableMapOf()

    init {
        subscribe<ImportSuccessEvent> { event ->
            contentController.replaceData(event.translations, event.languages)
            event.languages.forEach { language ->
                if (!formTranslations.containsKey(language)) {
                    formFieldset?.add(
                        addLanguageToForm(language)
                    )
                }
            }
        }
    }

    override val root = borderpane {
        center {
            tableView = tableview(contentController.data) {
                maxWidth = 520.0
                minWidth = 320.0
                column("Id", TranslationItem::idProperty) {
                    cellDecorator {
                        style {
                            // TODO Check for translations nullity
                        }
                    }
                }
                column("Key", TranslationItem::keyProperty) {
                    makeEditable()
                    remainingWidth()
                    cellDecorator {
                        style {
                            // TODO Check for null, for key unicity
                            if (it.isNullOrEmpty()) {
                                backgroundColor += Color(0.901, 0.360, 0.278, 0.6)
                            }
                            if (contentController.data.filter { element -> element.key == it }.size > 1) {
                                backgroundColor += Color(0.0, 0.360, 0.278, 0.6)
                            }
                        }
                    }
                    textProperty().addListener { _, _, new ->
                        contentController.currentSelectedItem.key = new
                    }
                }
                selectionModel.selectedItemProperty().onChange {
                    if (it != null) {
                        contentController.currentSelectedItem = it
                        if (formKey != null) bindForm()
                    }
                }
                smartResize()
            }
        }
        right {
            form {
                minWidth = 400.0
                prefWidth = 1400.0
                formFieldset = fieldset("Edit translations") {
                    field("Key") {
                        formKey = label(contentController.currentSelectedItem.key)
                    }
                    contentController.currentSelectedItem.translations.keys.forEach { language ->
                        addLanguageToForm(language)
                    }
                }
            }
        }
        top {
            hbox {
                paddingAll = 10.0
                spacing = 10.0
                alignment = Pos.CENTER_LEFT
                button("Show Json") {
                    action {
                        println(contentController.data)
                        find<JsonPreviewFragment>().openModal(
                            stageStyle = StageStyle.UTILITY,
                            modality = Modality.APPLICATION_MODAL,
                            escapeClosesWindow = true,
                            owner = currentWindow,
                            block = false,
                            resizable = true
                        )
                    }
                }
                // Add button
                button("", imageview("images/add.png") {
                    fitHeight = 20.0
                    fitWidth = 20.0
                }) {
                    action {
                        fire(NewTranslationItemEvent)
                    }
                }
                // Remove button
                button("", imageview("images/bin.png") {
                    fitHeight = 20.0
                    fitWidth = 20.0
                }) {
                    action {
                        fire(RemoveTranslationItemEvent)
                    }
                }
            }
        }
    }

    private fun addLanguageToForm(language: String): Field {
        return field(language) {
            textfield(contentController.currentSelectedItem.translations[language]) {
                formTranslations[language] = this
                textProperty().addListener { _, _, new ->
                    contentController.currentSelectedItem.translations[language] = new
                }
            }
        }
    }

    private fun bindForm() {
        formKey?.text = contentController.currentSelectedItem.key
        formTranslations.forEach { (language, textField) ->
            textField.text = contentController.currentSelectedItem.translations[language]
        }
    }
}
