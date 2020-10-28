// KanaQuiz styles
import "../styles/main.scss";

// Page scripts
import { Home, Question } from "./pages";

// Utils scripts
import { RouteService, Page } from "./utils";

// Route service
export function RouteHandler(route: string): void {
  const pages: Array<Page> = [Home, Question];
  RouteService.ParseRoute(route, pages);
}
