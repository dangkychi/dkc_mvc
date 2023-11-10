// Khi tài liệu đã được tải hoàn toàn
$(document).ready(function () {
    // Cập nhật chiều cao của footer
    function updateFooterHeight() {
        const bodyHeight = $('body').height();
        const windowHeight = $(window).height();
        const footer = $('.footer');

        if (bodyHeight < windowHeight) {
            footer.css('position', 'absolute');
            footer.css('bottom', '0');
        } else {
            footer.css('position', 'relative');
        }
    }

    // Gọi hàm cập nhật footer height sau khi tài liệu tải xong và sau mỗi lần thay đổi kích thước cửa sổ
    updateFooterHeight();
    $(window).resize(updateFooterHeight);
});

/*window.UpdateShowAllComments = (showAll) => {
    // Gọi phương thức C# để cập nhật trạng thái
    DotNet.invokeMethodAsync('DuHoc', 'UpdateShowAllComments', showAll);
};*/

window.UpdateCommentCount = (count) => {
    CountofComment = count;
};
