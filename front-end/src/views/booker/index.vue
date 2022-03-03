<template>
  <div class="app-container">
    <el-table
      :data="tableData"
      stripe
      style="width: 100%">
      <el-table-column
        prop="userName"
        label="用户名"
        width="180px">
      </el-table-column>

      <el-table-column
        prop="name"
        label="真实姓名"
        width="180px">
      </el-table-column>

      <el-table-column
        prop="gender"
        label="性别"
        width="180px">
      </el-table-column>

      <el-table-column
        prop="phoneNum"
        label="手机号码"
        width="180px">
      </el-table-column>

      <el-table-column
        prop="timeOfRegister"
        label="注册时间"
        width="180px">
      </el-table-column>

      <el-table-column
        prop="age"
        label="年龄"
        width="180px">
      </el-table-column>
    </el-table>

    <el-table-column label="操作" align="center" min-width="150">
      <el-button type="info" @click="back">上一页</el-button>
      <el-button type="info" @click="next">下一页</el-button>
    </el-table-column>
  </div>
</template>

<script>
import axios from 'axios'
export default {
  data() {
    return {
      cur: 1,
      size: 5,
      tableData: []
    }
  },
  mounted: function() {
    this.show()
  },
  methods: {
    back() {
      const that = this
      if (this.cur === 1) {
        this.$message('已是第一页')
      } else {
        this.cur--
        axios.get('https://localhost:7162/api/bookers/getallbookers', {
            params: {
              PageNumber: this.cur,
              PageSize: this.size
            }
          }
        ).then(res => {
            console.log(res)
            that.tableData = res
          }
        )
      }
    },
    next() {
      const that = this
      this.cur++
      axios.get('https://localhost:7162/api/bookers/getallbookers', {
          params: {
            PageNumber: this.cur,
            PageSize: this.size
          }
        }
      ).then(res => {
          const array = res
          if (array === undefined || array === null || array.length <= 0) {
            this.cur--
            this.$message('已经是最后一页了！')
          } else {
            that.tableData = array
          }
        }
      )
    },

    show() {
      const that = this
      axios.get('https://localhost:7162/api/bookers/getallbookers', {
        params: {
          PageNumber: that.cur,
          PageSize: that.size
        }
      }, {}).then(res => {
        console.log(res)
        that.tableData = res
      })
    }
  }
}
</script>
