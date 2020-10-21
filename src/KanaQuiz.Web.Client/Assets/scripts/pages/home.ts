import tippy from "tippy.js";
import { Page } from "../utils";

class HomePage implements Page {
  Name = "Home";
  public OnInit(): void {
    tippy("#blop", {
      content: "My tooltip!",
    });
  }
}

export const Home = new HomePage();
