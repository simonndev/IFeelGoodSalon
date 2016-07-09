import component from './main.component';
import routeConfig from './main.route';

let mainModule = angular.module('IFeelGoodSalon.Components.Main', [])
  .config(routeConfig)
  .component('main', component);

export default mainModule;
