#! /bin/sh

# 关闭已启动进程
kill -9 $(pgrep appname)

# 转到源码目录
cd ~/appDir/

# 拉取最新源码
git pull https://github.com/xxx/appname.git

# 转至相应目录，build、……
cd lunchDir/

# lunch （& 设置后台运行）
./app &