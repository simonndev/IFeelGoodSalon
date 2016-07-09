import leftSidebarComponent from './left/left.component';
import rightSidebarComponent from './right/right.component';

let sidebarModule = angular.module('IFeelGoodSalon.common.sidebar', [])
  .component('leftSidebar', leftSidebarComponent)
  .component('rightSidebar', rightSidebarComponent);

export default sidebarModule;
