package main

import content.ContentView
import event.ExportRequest
import event.NewTranslationItemEvent
import event.RemoveTranslationItemEvent
import tornadofx.*

class MainView : View() {
    private val mainController by inject<MainController>()
    private val contentView by inject<ContentView>()

    override val root = borderpane {
        top {
            menubar {
                menu("File") {
                    item("Create", "Shortcut+N").action {
                        mainController.createNewTranslationDirectory()
                    }
                    item("Import", "Shortcut+I").action {
                        mainController.import()
                    }
                    item("Export", "Shortcut+E").action {
                        fire(ExportRequest)
                    }
                    /*item("Save", "Shortcut+S").action {
                        mainController.save()
                    }*/
                    separator()
                    item("Quit").action {
                        mainController.exit()
                    }
                }
                menu("Items") {
                    item("Add", "Shortcut+N").action {
                        fire(NewTranslationItemEvent)
                    }
                    item("Remove selected", "Shortcut+R").action {
                        fire(RemoveTranslationItemEvent)
                    }
                }
                menu("Help") {
                    item("Get help").action { mainController.getHelp() }
                }
            }
        }
        center = contentView.root
    }
}
