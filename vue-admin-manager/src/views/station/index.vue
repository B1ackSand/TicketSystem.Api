<template>
  <div class="app-container">
    <!--    修改用户-->
    <el-dialog :visible.sync="dialogFormVisible">
      <el-form
        ref="dataForm"
        :model="bookerForm"
        label-position="left"
        label-width="90px"
        style="width: 400px; margin-left:50px;"
      >
        <el-form-item prop="stationName" label="站点名">
          <el-input v-model="bookerForm.stationName" />
        </el-form-item>

        <el-form-item prop="isTerminal" label="是否为终止站">
          <el-input v-model="bookerForm.isTerminal" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">返回</el-button>
        <el-button type="primary" @click="updateData()">修改</el-button>
      </div>
    </el-dialog>

    <!--    添加用户-->
    <el-dialog :visible.sync="dialogAddVisible">
      <el-form
        ref="dataForm"
        :model="bookerForm"
        label-position="left"
        label-width="90px"
        style="width: 400px; margin-left:50px;"
      >
        <el-form-item prop="stationName" label="站点名">
          <el-input v-model="bookerForm.stationName" />
        </el-form-item>

        <el-form-item prop="isTerminal" label="是否为终止站">
          <el-input v-model="bookerForm.isTerminal" />
        </el-form-item>

      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="cancelAddUser;dialogAddVisible = false;">返回</el-button>
        <el-button type="primary" @click="addUser">添加</el-button>
      </div>
    </el-dialog>

    <el-table
      :data="tableData"
      stripe
      style="width: 100%"
    >
      <el-table-column
        prop="stationId"
        label="停靠站ID"
        width="500px"
      />

      <el-table-column
        prop="stationName"
        label="站点名"
        width="300px"
      />

      <el-table-column
        prop="isTerminal"
        label="是否为终止站"
        width="180px"
        :formatter="formatBoolean"
      />

      <el-table-column label="修改操作" align="center" min-width="150" width="660">
        <template slot-scope="scope">
          <el-button type="info" @click="modifyUser(scope.row);dialogFormVisible = true;">修改</el-button>
          <el-button type="info" @click="deleteUser(scope.row)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>

    <el-table-column label="操作" align="center" min-width="150">
      <el-button type="info" @click="back">上一页</el-button>
      <el-button type="info" @click="next">下一页</el-button>
      <el-button type="info" @click="cancelAddUser();dialogAddVisible = true;">添加</el-button>
    </el-table-column>
  </div>
</template>

<script>
import axios from 'axios'
export default {
  data() {
    return {
      cur: 1,
      size: 10,
      bookerForm: {
        stationId: '',
        stationName: '',
        isTerminal: false.toString()
      },
      dialogFormVisible: false,
      dialogAddVisible: false,
      tableData: [],
      addData: []
    }
  },
  mounted: function() {
    this.show()
  },
  methods: {
    back() {
      const that = this
      if (this.cur === 1) {
        this.$message('已经是第一页了')
      } else {
        this.cur--
        axios.get('https://ticket.blacksand.top/api/stations', {
          params: {
            PageNumber: this.cur,
            PageSize: this.size
          }
        }
        ).then(res => {
          console.log(res.data)
          that.tableData = res.data
        }
        )
      }
    },
    next() {
      const that = this
      this.cur++
      axios.get('https://ticket.blacksand.top/api/stations', {
        params: {
          PageNumber: this.cur,
          PageSize: this.size
        }
      }
      ).then(res => {
        const array = res.data
        if (array === undefined || array === null || array.length <= 0) {
          this.cur--
          this.$message('已经是最后一页了')
        } else {
          that.tableData = array
        }
      }
      )
    },

    show() {
      const that = this
      axios.get('https://ticket.blacksand.top/api/stations', {
        params: {
          PageNumber: that.cur,
          PageSize: that.size
        }
      }, {}).then(res => {
        console.log(res.data)
        that.tableData = res.data
      })
    },

    cancelAddUser() {
      this.bookerForm = {
        stationId: '',
        stationName: '',
        isTerminal: false
      }
    },
    modifyUser(val) {
      this.bookerForm = {
        stationId: val.stationId,
        stationName: val.stationName,
        isTerminal: val.isTerminal
      }
    },

    deleteUser(val) {
      var ID = val
      const that = this
      axios.delete('https://ticket.blacksand.top/api/stations/deleteStation', {
        params: {
          stationId: ID.stationId
        }
      }, {}).then(res => {
        that.$message(res.status + '删除成功')
        that.tableData = []
        that.Data = []
        this.show()
      }
      )
    },

    addUser() {
      const that = this
      axios.post('https://ticket.blacksand.top/api/stations', {
        stationName: that.bookerForm.stationName,
        isTerminal: that.bookerForm.isTerminal
      }, {
      }).then(res => {
        if (res.status === 201) {
          that.$message('已创建成功')
          that.bookerForm = []
          that.dialogAddVisible = false
          this.show()
        } else {
          that.$message('创建失败')
        }
      }
      )
    },

    updateData() {
      const that = this
      axios.put('https://ticket.blacksand.top/api/stations/updateStation', {
        stationName: that.bookerForm.stationName,
        isTerminal: that.bookerForm.isTerminal
      }, {
        params: {
          stationId: that.bookerForm.stationId
        }
      }).then(res => {
        if (res.status === 204 || res.status === 201) {
          that.$message('已提交成功')
          that.dialogFormVisible = false
          that.tableData = []
          this.show()
        } else {
          that.$message('提交失败')
        }
      }
      )
    },
    /* 布尔值格式化：cellValue为后台返回的值
*/
    formatBoolean: function(row, column, cellValue) {
      var ret = '' // 你想在页面展示的值
      if (cellValue) {
        ret = '是' // 根据自己的需求设定
      } else {
        ret = '否'
      }
      return ret
    }
  }
}
</script>
