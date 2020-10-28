import tippy from "tippy.js";
import { Page } from "../utils";

class HomePage implements Page {
  Name = "Home";
  public OnInit(): void {
    tippy("#tippy-hello", {
      content: "Hello",
    });
  }
}

export const Home = new HomePage();
