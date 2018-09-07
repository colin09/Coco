mvn 

> command
    -v 
    compile 编译
    test 测试
    packge 打包

    clean 删除target
    install 安装jar包到本地仓库中


> 创建目录两种方式：
    1. archetype:generate 
    2. archetype:generate -DgroupId=组织（公司地址反写+项目名）
                          -DartifactId=项目名-模块名
                          -Dversion=版本号
                          -Dpackage=代码所在的包名

> 镜像仓库
    MAVEN_HOME > conf > settings.xml > mirror (镜像仓库)

> 本地仓库
    currrentUser > .m2\repository  (默认地址)
    MAVEN_HOME > settings.xml > localRepository

 


> 启动
    1，src/main/java/×/*application , 右键run
    2, cmd$: mvn spring-boot:run
    3, cmd$: mvn install > cd target > java -jar *.jar
    4, cmd$: mvn install > cd target > java -jar *.jar --spring.profiles.active=prd 


> 

snapshot 快照
alpha 内测
beta 公测
release 稳定
GA  正式发布
