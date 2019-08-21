function MediaPickerController(editorService) {
  var vm = this;

  vm.openMediaPicker = openMediaPicker;

  function openMediaPicker() {
    var mediaPickerOptions = {
      multiPicker: true,
      submit: function(model) {
        editorService.close();
      },
      close: function() {
        editorService.close();
      }
    };
    editorService.mediaPicker(mediaPickerOptions);
  }
}
