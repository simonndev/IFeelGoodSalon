import controller from './main.controller';

let rootComponent = {
  restrict: 'E',
  bindings: {},
  templateUrl: 'app/components/main/main.html',
  controller,
  controllerAs: 'vm'
};

export default rootComponent;
