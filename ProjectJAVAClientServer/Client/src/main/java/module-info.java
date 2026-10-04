module be.hers.client {
    requires javafx.controls;
    requires javafx.fxml;
    requires javafx.graphics;
    requires java.net.http;
    requires java.xml;
    requires javafx.base;


    opens be.hers.client to javafx.fxml;
    exports be.hers.client;
}