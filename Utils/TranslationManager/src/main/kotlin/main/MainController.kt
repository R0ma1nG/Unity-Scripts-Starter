package main

import event.*
import javafx.stage.DirectoryChooser
import objects.TranslationItem
import objects.json.JsonTranslationKeyValue
import objects.json.JsonTranslationList
import tornadofx.Controller
import tornadofx.toObservable
import java.awt.Desktop
import java.io.File
import java.net.URI
import kotlin.system.exitProcess
import com.google.gson.GsonBuilder

class MainController : Controller() {
    private var directoryPath: String? = null
    private val gson = GsonBuilder().setPrettyPrinting().create()

    init {
        subscribe<ExportEvent> { event ->
            println("Data being exported")
            val directoryChooser = DirectoryChooser()
            val dir = directoryChooser.showDialog(null)
            event.languages.forEach { language ->
                val file = File("${dir.absolutePath}/strings_$language.json")
                file.createNewFile()
                val translations = mutableListOf<JsonTranslationKeyValue>()
                event.data.forEach {
                    translations.add(JsonTranslationKeyValue(it.key, it.translations.getOrDefault(language, "")))
                }
                val result = JsonTranslationList(translations)
                file.writeText(gson.toJson(result))
            }
        }
        subscribe<SaveEvent> {
            println("Saving data not implemented")
        }
    }

    fun createNewTranslationDirectory() {
        val directoryChooser = DirectoryChooser();
        val dir = directoryChooser.showDialog(null)
        if (dir != null && dir.listFilesSafe().isNotEmpty()) {
            println("Directory is not empty")
            return
        }
    }

    fun exit() {
        exitProcess(0)
    }

    fun getHelp() {
        Desktop.getDesktop().browse(URI("https://quiche-entertainment.com/"))
    }

    fun import() {
        // Select the files
        val files = selectImportFiles() ?: return

        // Import the data
        val translations = mutableMapOf<String, MutableMap<String, String>>()
        val languages = mutableListOf<String>()
        files.forEach { file ->
            val obj = gson.fromJson(file.readText(), JsonTranslationList::class.java)
            val language = file.name.removePrefix("strings_").removeSuffix(".json")
            languages.add(language)
            obj.items.forEach { item ->
                translations.putIfAbsent(item.key, mutableMapOf())
                translations[item.key]?.put(language, item.value)
            }
        }

        // Convert to UI Model data
        var i = 0
        val res = translations.map {
            TranslationItem(i++, it.key, it.value.toObservable())
        }

        // Send data loaded event
        fire(ImportSuccessEvent(res, languages))
    }

    private fun File.listFilesSafe(): Array<File> {
        return if (this.listFiles() != null) this.listFiles()!! else arrayOf()
    }

    private fun selectImportFiles(): List<File>? {
        val directoryChooser = DirectoryChooser()
        val dir = directoryChooser.showDialog(null)
        if (dir == null) {
            println("Cancel import directory choice.")
            return null
        }
        val files = dir.listFilesSafe().filter { it.extension == "json" }
        if (files.isEmpty()) {
            println("Directory is not empty")
            return null
        }
        directoryPath = dir.absolutePath
        return files
    }
}
