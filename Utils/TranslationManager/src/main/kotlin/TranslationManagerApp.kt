import javafx.scene.paint.Color
import javafx.stage.Stage
import main.MainView
import tornadofx.App
import tornadofx.UIComponent
import tornadofx.launch

class TranslationManagerApp : App(MainView::class) {
    override fun start(stage: Stage) {
        stage.minWidth = 720.0
        stage.minHeight = 576.0
        stage.width = 720.0
        stage.height = 576.0
        super.start(stage)
    }

    override fun createPrimaryScene(view: UIComponent) = super.createPrimaryScene(view).apply {
        fill = Color.valueOf("#EDEDED")
    }
}

fun main(args: Array<String>) {
    launch<TranslationManagerApp>(args)
}
