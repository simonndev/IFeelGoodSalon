import navbarComponent from './navbar.component';

let navbarModule = angular.module('navbar', [])
  .run(calendarRun)
  .directive('navbar', calendarComponent)
  .service('navbarService', calendarService);

export default navbarModule;
