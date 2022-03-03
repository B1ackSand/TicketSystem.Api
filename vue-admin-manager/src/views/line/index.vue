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

        <el-form-item prop="startTerminal" label="起始站">
          <el-input v-model="bookerForm.startTerminal" />
        </el-form-item>

        <el-form-item prop="endTerminal" label="终点站">
          <el-input v-model="bookerForm.endTerminal" />
        </el-form-item>

        <el-form-item prop="stopStation" label="路线详情">
          <el-input v-model="bookerForm.stopStation" />
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
        <el-form-item prop="startTerminal" label="起始站">
          <el-input v-model="bookerForm.startTerminal" />
        </el-form-item>

        <el-form-item prop="endTerminal" label="终点站">
          <el-input v-model="bookerForm.endTerminal" />
        </el-form-item>

        <el-form-item prop="stopStation" label="路线详情">
          <el-input v-model="bookerForm.stopStation" />
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
        prop="lineId"
        label="路线Id"
        width="300px"
      />

      <el-table-column
        prop="startTerminal"
        label="起始站"
        width="180px"
      />

      <el-table-column
        prop="endTerminal"
        label="终点站"
        width="180px"
      />

      <el-table-column
        prop="stopStation"
        label="路线详情"
        width="800px"
      />

      <el-table-column label="修改操作" align="center" min-width="150" width="200">
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
      size: 5,
      bookerForm: {
        lineId: '',
        startTerminal: '',
        endTerminal: '',
        stopStation: ''
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
        this.$message('已是第一页')
      } else {
        this.cur--
        axios.get('https://localhost:7162/api/lines/getAllLines', {
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
      axios.get('https://localhost:7162/api/lines/getAllLines', {
        params: {
          PageNumber: this.cur,
          PageSize: this.size
        }
      }
      ).then(res => {
        const array = res.data
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
      axios.get('https://localhost:7162/api/lines/getAllLines', {
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
        lineId: '',
        startTerminal: '',
        endTerminal: '',
        stopStation: ''
      }
    },

    modifyUser(val) {
      this.bookerForm = {
        lineId: val.lineId,
        startTerminal: val.startTerminal,
        endTerminal: val.endTerminal,
        stopStation: val.stopStation
      }
    },

    deleteUser(val) {
      var ID = val
      const that = this
      axios.delete('https://localhost:7162/api/lines/deleteLine', {
        params: {
          lineId: ID.lineId
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
      axios.post('https://localhost:7162/api/lines', {
        startTerminal: that.bookerForm.startTerminal,
        endTerminal: that.bookerForm.endTerminal,
        stopStation: that.bookerForm.stopStation
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
      axios.put('https://localhost:7162/api/lines/updateLine', {
        startTerminal: that.bookerForm.startTerminal,
        endTerminal: that.bookerForm.endTerminal,
        stopStation: that.bookerForm.stopStation
      }, {
        params: {
          lineId: that.bookerForm.lineId
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
    }
  }
}
</script>
