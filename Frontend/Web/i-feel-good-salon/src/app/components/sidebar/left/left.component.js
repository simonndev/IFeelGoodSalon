import controller from './left.controller';

let leftSidebarComponent = {
  restrict: 'E',
  bindings: {},
  templateUrl: 'app/components/sidebar/left/left.html',
  controller,
  controllerAs: 'vm'
};

export default leftSidebarComponent;
