var currentDirId = 'root';
var currentDirName = '安全制度管理';
var currentFiles = [];
var currentPreviewFile = null;

layui.use(['layer'], function() {
    var layer = layui.layer;
    
    init();
    
    function init() {
        loadTree();
        bindEvents();
    }
    
    function loadTree() {
        $.ajax({
            url: '/api/SafetySystem/GetDirectoryTree',
            type: 'GET',
            success: function(res) {
                if (res.Success) {
                    var tree = res.Data;
                    renderTree(tree);
                } else {
                    layer.msg(res.Message || '加载失败', { icon: 2 });
                }
            },
            error: function() {
                layer.msg('加载失败', { icon: 2 });
            }
        });
    }
    
    function renderTree(node) {
        var container = $('#treeContainer');
        container.empty();
        
        function renderNode(n, level) {
            var indent = '';
            for (var i = 0; i < level; i++) {
                indent += '<span class="tree-indent"></span>';
            }
            
            var hasChildren = n.HasChildren || (n.Children && n.Children.length > 0);
            var expandIcon = hasChildren ? '<span class="tree-expand">+</span>' : '<span class="tree-expand"> </span>';
            
            var iconClass = 'layui-icon-folder';
            if (hasChildren) {
                iconClass = 'layui-icon-folder-open';
            }
            
            var hasFilesClass = n.HasFiles ? 'has-files' : '';
            var activeClass = n.Id === currentDirId ? 'active' : '';
            
            var div = $('<div>')
                .addClass('tree-node ' + hasFilesClass + ' ' + activeClass)
                .attr('data-id', n.Id)
                .attr('data-name', n.Name)
                .attr('data-haschildren', hasChildren)
                .html(indent + expandIcon + '<i class="layui-icon ' + iconClass + ' tree-icon"></i>' + n.Name);
            
            div.click(function(e) {
                e.stopPropagation();
                var id = $(this).attr('data-id');
                var name = $(this).attr('data-name');
                
                $('.tree-node').removeClass('active');
                $(this).addClass('active');
                
                currentDirId = id;
                currentDirName = name;
                $('#contentTitle').text(name);
                
                loadFiles(id);
            });
            
            div.contextmenu(function(e) {
                e.preventDefault();
                showContextMenu(e, n);
            });
            
            container.append(div);
            
            if (hasChildren && n.Children) {
                n.Children.forEach(function(child) {
                    renderNode(child, level + 1);
                });
            }
        }
        
        renderNode(node, 0);
        
        $('.tree-expand').click(function(e) {
            e.stopPropagation();
            var $this = $(this);
            var text = $this.text();
            if (text === '+') {
                $this.text('-');
            } else {
                $this.text('+');
            }
        });
    }
    
    function loadFiles(directoryId) {
        $.ajax({
            url: '/api/SafetySystem/GetFilesByDirectory',
            type: 'GET',
            data: { directoryId: directoryId },
            success: function(res) {
                if (res.Success) {
                    currentFiles = res.Data || [];
                    renderFileList(currentFiles);
                } else {
                    layer.msg(res.Message || '加载文件失败', { icon: 2 });
                }
            },
            error: function() {
                layer.msg('加载文件失败', { icon: 2 });
            }
        });
    }
    
    function renderFileList(files) {
        var container = $('#contentBody');
        container.empty();
        
        if (!files || files.length === 0) {
            container.html('<div class="file-list"><div class="file-list-header">该目录暂无文件</div></div>');
            return;
        }
        
        var fileList = $('<div>').addClass('file-list');
        var header = $('<div>').addClass('file-list-header').text(currentDirName + ' - 文件列表');
        fileList.append(header);
        
        files.forEach(function(file) {
            var fileItem = $('<div>').addClass('file-item').attr('data-id', file.Id);
            
            var icon = getFileIcon(file.FileType);
            var iconColor = getFileIconColor(file.FileType);
            
            var fileIcon = $('<div>').addClass('file-icon').html('<i class="layui-icon ' + icon + '" style="color:' + iconColor + ';"></i>');
            
            var fileInfo = $('<div>').addClass('file-info');
            var fileName = $('<div>').addClass('file-name').text(file.FileName);
            var fileMeta = $('<div>').addClass('file-meta').text('版本: ' + file.Version + ' | ' + file.FileSizeText + ' | ' + formatDate(file.UpdateTime));
            fileInfo.append(fileName).append(fileMeta);
            
            var fileActions = $('<div>').addClass('file-actions');
            var previewBtn = $('<button>').addClass('layui-btn layui-btn-xs').html('<i class="layui-icon layui-icon-eye"></i> 预览');
            var deleteBtn = $('<button>').addClass('layui-btn layui-btn-xs layui-btn-danger').html('<i class="layui-icon layui-icon-delete"></i> 删除');
            
            previewBtn.click(function(e) {
                e.stopPropagation();
                previewFile(file);
            });
            
            deleteBtn.click(function(e) {
                e.stopPropagation();
                deleteFile(file);
            });
            
            fileActions.append(previewBtn).append(deleteBtn);
            
            fileItem.click(function() {
                previewFile(file);
            });
            
            fileItem.append(fileIcon).append(fileInfo).append(fileActions);
            fileList.append(fileItem);
        });
        
        container.append(fileList);
    }
    
    function getFileIcon(fileType) {
        var icons = {
            'image': 'layui-icon-picture',
            'pdf': 'layui-icon-file-b',
            'word': 'layui-icon-file',
            'excel': 'layui-icon-table',
            'ppt': 'layui-icon-template',
            'other': 'layui-icon-file'
        };
        return icons[fileType] || icons['other'];
    }
    
    function getFileIconColor(fileType) {
        var colors = {
            'image': '#1e9fff',
            'pdf': '#ff5722',
            'word': '#1e9fff',
            'excel': '#16baaa',
            'ppt': '#ffb800',
            'other': '#999'
        };
        return colors[fileType] || colors['other'];
    }
    
    function formatDate(dateStr) {
        if (!dateStr) return '';
        var date = new Date(dateStr);
        return date.getFullYear() + '-' + 
            String(date.getMonth() + 1).padStart(2, '0') + '-' + 
            String(date.getDate()).padStart(2, '0') + ' ' +
            String(date.getHours()).padStart(2, '0') + ':' +
            String(date.getMinutes()).padStart(2, '0');
    }
    
    function previewFile(file) {
        currentPreviewFile = file;
        var container = $('#contentBody');
        container.empty();
        
        var previewContainer = $('<div>').addClass('preview-container');
        
        var previewHeader = $('<div>').addClass('preview-header');
        var previewTitle = $('<div>').addClass('preview-title').text(file.FileName);
        var backBtn = $('<button>').addClass('layui-btn layui-btn-xs layui-btn-primary').html('<i class="layui-icon layui-icon-left"></i> 返回列表');
        previewHeader.append(previewTitle).append(backBtn);
        
        backBtn.click(function() {
            loadFiles(currentDirId);
        });
        
        var previewBody = $('<div>').addClass('preview-body');
        
        if (file.FileType === 'image') {
            var img = $('<img>').addClass('preview-image').attr('src', file.FileUrl);
            previewBody.append(img);
        } else if (file.FileType === 'pdf') {
            var iframe = $('<iframe>').addClass('preview-pdf').attr('src', file.FileUrl);
            previewBody.append(iframe);
        } else {
            var officeDiv = $('<div>').addClass('preview-office');
            officeDiv.html('<i class="layui-icon ' + getFileIcon(file.FileType) + '" style="font-size: 64px; color: #ddd;"></i><p style="margin-top: 10px;">无法在线预览该文件格式</p><button class="layui-btn" style="margin-top: 10px;" onclick="window.open(\'' + file.FileUrl + '\')">下载查看</button>');
            previewBody.append(officeDiv);
        }
        
        previewContainer.append(previewHeader).append(previewBody);
        container.append(previewContainer);
    }
    
    function deleteFile(file) {
        layer.confirm('确定要删除文件 "' + file.FileName + '" 吗？', function(index) {
            layer.close(index);
            $.ajax({
                url: '/api/SafetySystem/DeleteFile',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ id: file.Id }),
                success: function(res) {
                    if (res.Success) {
                        layer.msg('删除成功', { icon: 1 });
                        loadFiles(currentDirId);
                        loadTree();
                    } else {
                        layer.msg(res.Message || '删除失败', { icon: 2 });
                    }
                },
                error: function() {
                    layer.msg('删除失败', { icon: 2 });
                }
            });
        });
    }
    
    function showContextMenu(e, node) {
        var menu = $('#contextMenu');
        menu.attr('data-node-id', node.Id);
        menu.attr('data-node-name', node.Name);
        
        menu.css({
            left: e.pageX + 'px',
            top: e.pageY + 'px',
            display: 'block'
        });
    }
    
    function bindEvents() {
        $(document).click(function() {
            $('#contextMenu').hide();
        });
        
        $('#contextMenu').click(function(e) {
            e.stopPropagation();
            var action = $(e.target).attr('data-action');
            var nodeId = $(this).attr('data-node-id');
            var nodeName = $(this).attr('data-node-name');
            
            $(this).hide();
            
            if (action === 'addFile') {
                openUploadModal(nodeId, nodeName);
            } else if (action === 'addDir') {
                openAddDirModal(nodeId, nodeName);
            } else if (action === 'deleteDir') {
                deleteDirectory(nodeId, nodeName);
            }
        });
        
        $('#btnAddDir').click(function() {
            openAddDirModal(currentDirId, currentDirName);
        });
        
        $('#btnAddFile').click(function() {
            openUploadModal(currentDirId, currentDirName);
        });
        
        $('#btnSelectFile').click(function() {
            $('#fileInput').click();
        });
        
        $('#fileInput').change(function(e) {
            var file = e.target.files[0];
            if (file) {
                showFileInfo(file);
            }
        });
        
        $('#btnCancelUpload').click(function() {
            closeUploadModal();
        });
        
        $('#btnConfirmUpload').click(function() {
            uploadFile();
        });
        
        $('#btnCancelAddDir').click(function() {
            closeAddDirModal();
        });
        
        $('#btnConfirmAddDir').click(function() {
            addDirectory();
        });
    }
    
    function showFileInfo(file) {
        var previewBox = $('#filePreviewBox');
        var previewInfo = $('#filePreviewInfo');
        
        var icon = getFileIconByExtension(file.name);
        var iconColor = getFileIconColorByExtension(file.name);
        
        previewBox.html('<i class="layui-icon ' + icon + '" style="font-size: 48px; color:' + iconColor + ';"></i>');
        
        var infoHtml = '<p><strong>文件名：</strong>' + file.name + '</p>' +
                       '<p><strong>文件大小：</strong>' + formatFileSize(file.size) + '</p>' +
                       '<p><strong>文件类型：</strong>' + file.type + '</p>' +
                       '<p><strong>修改日期：</strong>' + new Date(file.lastModified).toLocaleString() + '</p>';
        
        previewInfo.html(infoHtml);
    }
    
    function getFileIconByExtension(fileName) {
        var ext = fileName.split('.').pop().toLowerCase();
        var icons = {
            'jpg': 'layui-icon-picture',
            'jpeg': 'layui-icon-picture',
            'png': 'layui-icon-picture',
            'gif': 'layui-icon-picture',
            'bmp': 'layui-icon-picture',
            'pdf': 'layui-icon-file-b',
            'doc': 'layui-icon-file',
            'docx': 'layui-icon-file',
            'xls': 'layui-icon-table',
            'xlsx': 'layui-icon-table',
            'ppt': 'layui-icon-template',
            'pptx': 'layui-icon-template'
        };
        return icons[ext] || 'layui-icon-file';
    }
    
    function getFileIconColorByExtension(fileName) {
        var ext = fileName.split('.').pop().toLowerCase();
        var colors = {
            'jpg': '#1e9fff',
            'jpeg': '#1e9fff',
            'png': '#1e9fff',
            'gif': '#1e9fff',
            'bmp': '#1e9fff',
            'pdf': '#ff5722',
            'doc': '#1e9fff',
            'docx': '#1e9fff',
            'xls': '#16baaa',
            'xlsx': '#16baaa',
            'ppt': '#ffb800',
            'pptx': '#ffb800'
        };
        return colors[ext] || '#999';
    }
    
    function formatFileSize(bytes) {
        if (bytes < 1024) return bytes + ' B';
        if (bytes < 1024 * 1024) return (bytes / 1024.0).toFixed(2) + ' KB';
        if (bytes < 1024 * 1024 * 1024) return (bytes / (1024.0 * 1024)).toFixed(2) + ' MB';
        return (bytes / (1024.0 * 1024 * 1024)).toFixed(2) + ' GB';
    }
    
    function openUploadModal(directoryId, directoryName) {
        $('#uploadDirectoryId').val(directoryId);
        $('#fileInput').val('');
        $('#uploadRemark').val('');
        
        var previewBox = $('#filePreviewBox');
        previewBox.html('<i class="layui-icon layui-icon-file" style="font-size: 48px; color: #ddd;"></i><div id="filePreviewInfo" class="file-preview-info"></div>');
        
        $('#uploadModal').show();
    }
    
    function closeUploadModal() {
        $('#uploadModal').hide();
    }
    
    function uploadFile() {
        var fileInput = $('#fileInput')[0];
        var file = fileInput.files[0];
        
        if (!file) {
            layer.msg('请选择文件', { icon: 2 });
            return;
        }
        
        var directoryId = $('#uploadDirectoryId').val();
        var remark = $('#uploadRemark').val();
        
        var formData = new FormData();
        formData.append('file', file);
        formData.append('directoryId', directoryId);
        formData.append('remark', remark);
        
        $.ajax({
            url: '/api/SafetySystem/UploadFile',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            beforeSend: function() {
                layer.load();
            },
            success: function(res) {
                layer.closeAll('loading');
                if (res.Success) {
                    layer.msg('上传成功', { icon: 1 });
                    closeUploadModal();
                    loadFiles(currentDirId);
                    loadTree();
                } else {
                    layer.msg(res.Message || '上传失败', { icon: 2 });
                }
            },
            error: function() {
                layer.closeAll('loading');
                layer.msg('上传失败', { icon: 2 });
            }
        });
    }
    
    function openAddDirModal(parentId, parentName) {
        $('#addDirParentId').val(parentId);
        $('#addDirParentName').val(parentName);
        $('#addDirName').val('');
        $('#addDirRemark').val('');
        
        $('#addDirModal').show();
    }
    
    function closeAddDirModal() {
        $('#addDirModal').hide();
    }
    
    function addDirectory() {
        var parentId = $('#addDirParentId').val();
        var name = $('#addDirName').val().trim();
        var remark = $('#addDirRemark').val();
        
        if (!name) {
            layer.msg('请输入目录名称', { icon: 2 });
            return;
        }
        
        $.ajax({
            url: '/api/SafetySystem/AddDirectory',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ name: name, parentId: parentId, remark: remark }),
            success: function(res) {
                if (res.Success) {
                    layer.msg('添加成功', { icon: 1 });
                    closeAddDirModal();
                    loadTree();
                } else {
                    layer.msg(res.Message || '添加失败', { icon: 2 });
                }
            },
            error: function() {
                layer.msg('添加失败', { icon: 2 });
            }
        });
    }
    
    function deleteDirectory(id, name) {
        layer.confirm('确定要删除目录 "' + name + '" 吗？删除后该目录下的所有子目录和文件也将被删除。', function(index) {
            layer.close(index);
            $.ajax({
                url: '/api/SafetySystem/DeleteDirectory',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ id: id }),
                success: function(res) {
                    if (res.Success) {
                        layer.msg('删除成功', { icon: 1 });
                        loadTree();
                        $('#contentBody').html('<div class="file-list"><div class="file-list-header">请选择左侧目录查看文件</div></div>');
                        $('#contentTitle').text('安全制度管理');
                        currentDirId = 'root';
                        currentDirName = '安全制度管理';
                    } else {
                        layer.msg(res.Message || '删除失败', { icon: 2 });
                    }
                },
                error: function() {
                    layer.msg('删除失败', { icon: 2 });
                }
            });
        });
    }
});