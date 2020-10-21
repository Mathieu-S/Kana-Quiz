import { Page } from "./page";

export class RouteService {
  public static ParseRoute(route: string, pages: Array<Page>): void {
    let actualPage: Page;

    if (route.length == 0) {
      actualPage = pages.find((x) => x.Name == "Home");
    } else {
      actualPage = pages.find((x) => x.Name.toLowerCase() == route);
    }

    actualPage.OnInit();
  }
}
