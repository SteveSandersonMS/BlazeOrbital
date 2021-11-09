using SQLitePCL;
using System.Runtime.InteropServices;

namespace BlazeOrbital.ManufacturingHub;

// As an alternative to declaring all this stuff manually, you could add to your project:
//   <PackageReference Include="SQLitePCLRaw.Bundle_e_sqlite3" Version="2.0.6" />
// The drawback currently is that it would log a set of irrelevant warnings during each build.
// This should be fixed if the SQLitePCLRaw project is updated to include a WebAssembly-targeting package.

static class NativeMethods
{
	static NativeMethods()
    {
		// Linker workaround
		GC.KeepAlive(DateOnly.FromDateTime(DateTime.Now).AddDays(1).AddMonths(1).AddYears(1));
	}

	private const string SQLITE_DLL = "e_sqlite3";
	private const CallingConvention CALLING_CONVENTION = CallingConvention.Cdecl;

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_close(IntPtr db);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_close_v2(IntPtr db);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_enable_shared_cache(int enable);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe void sqlite3_interrupt(sqlite3 db);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_finalize(IntPtr stmt);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_reset(sqlite3_stmt stmt);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_clear_bindings(sqlite3_stmt stmt);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_stmt_status(sqlite3_stmt stm, int op, int resetFlg);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe byte* sqlite3_bind_parameter_name(sqlite3_stmt stmt, int index);

	//[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	//public static extern unsafe byte* sqlite3_column_database_name(sqlite3_stmt stmt, int index);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe byte* sqlite3_column_decltype(sqlite3_stmt stmt, int index);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe byte* sqlite3_column_name(sqlite3_stmt stmt, int index);

	//[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	//public static extern unsafe byte* sqlite3_column_origin_name(sqlite3_stmt stmt, int index);

	//[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	//public static extern unsafe byte* sqlite3_column_table_name(sqlite3_stmt stmt, int index);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe byte* sqlite3_column_text(sqlite3_stmt stmt, int index);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe byte* sqlite3_errmsg(sqlite3 db);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_db_readonly(sqlite3 db, byte* dbName);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe byte* sqlite3_db_filename(sqlite3 db, byte* att);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_prepare_v2(sqlite3 db, byte* pSql, int nBytes, out IntPtr stmt, out byte* ptrRemain);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_prepare_v3(sqlite3 db, byte* pSql, int nBytes, uint flags, out IntPtr stmt, out byte* ptrRemain);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_db_status(sqlite3 db, int op, out int current, out int highest, int resetFlg);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_complete(byte* pSql);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_compileoption_used(byte* pSql);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe byte* sqlite3_compileoption_get(int n);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_table_column_metadata(sqlite3 db, byte* dbName, byte* tblName, byte* colName, out byte* ptrDataType, out byte* ptrCollSeq, out int notNull, out int primaryKey, out int autoInc);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe byte* sqlite3_value_text(IntPtr p);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_enable_load_extension(sqlite3 db, int enable);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_limit(sqlite3 db, int id, int newVal);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_initialize();

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_shutdown();

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe byte* sqlite3_libversion();

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_libversion_number();

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_threadsafe();

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe byte* sqlite3_sourceid();

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe IntPtr sqlite3_malloc(int n);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe IntPtr sqlite3_realloc(IntPtr p, int n);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe void sqlite3_free(IntPtr p);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_stricmp(IntPtr p, IntPtr q);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_strnicmp(IntPtr p, IntPtr q, int n);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_open(byte* filename, out IntPtr db);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_open_v2(byte* filename, out IntPtr db, int flags, byte* vfs);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe IntPtr sqlite3_vfs_find(byte* vfs);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe long sqlite3_last_insert_rowid(sqlite3 db);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_changes(sqlite3 db);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_total_changes(sqlite3 db);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe long sqlite3_memory_used();

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe long sqlite3_memory_highwater(int resetFlag);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe long sqlite3_soft_heap_limit64(long n);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe long sqlite3_hard_heap_limit64(long n);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_status(int op, out int current, out int highwater, int resetFlag);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_busy_timeout(sqlite3 db, int ms);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_bind_blob(sqlite3_stmt stmt, int index, byte* val, int nSize, IntPtr nTransient);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_bind_zeroblob(sqlite3_stmt stmt, int index, int size);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_bind_double(sqlite3_stmt stmt, int index, double val);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_bind_int(sqlite3_stmt stmt, int index, int val);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_bind_int64(sqlite3_stmt stmt, int index, long val);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_bind_null(sqlite3_stmt stmt, int index);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_bind_text(sqlite3_stmt stmt, int index, byte* val, int nlen, IntPtr pvReserved);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_bind_text16(sqlite3_stmt stmt, int index, char* val, int nlen, IntPtr pvReserved);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_bind_parameter_count(sqlite3_stmt stmt);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_bind_parameter_index(sqlite3_stmt stmt, byte* strName);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_column_count(sqlite3_stmt stmt);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_data_count(sqlite3_stmt stmt);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_step(sqlite3_stmt stmt);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe byte* sqlite3_sql(sqlite3_stmt stmt);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe double sqlite3_column_double(sqlite3_stmt stmt, int index);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_column_int(sqlite3_stmt stmt, int index);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe long sqlite3_column_int64(sqlite3_stmt stmt, int index);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe IntPtr sqlite3_column_blob(sqlite3_stmt stmt, int index);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_column_bytes(sqlite3_stmt stmt, int index);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_column_type(sqlite3_stmt stmt, int index);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_aggregate_count(IntPtr context);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe IntPtr sqlite3_value_blob(IntPtr p);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_value_bytes(IntPtr p);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe double sqlite3_value_double(IntPtr p);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_value_int(IntPtr p);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe long sqlite3_value_int64(IntPtr p);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_value_type(IntPtr p);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe IntPtr sqlite3_user_data(IntPtr context);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe void sqlite3_result_blob(IntPtr context, IntPtr val, int nSize, IntPtr pvReserved);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe void sqlite3_result_double(IntPtr context, double val);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe void sqlite3_result_error(IntPtr context, byte* strErr, int nLen);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe void sqlite3_result_int(IntPtr context, int val);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe void sqlite3_result_int64(IntPtr context, long val);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe void sqlite3_result_null(IntPtr context);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe void sqlite3_result_text(IntPtr context, byte* val, int nLen, IntPtr pvReserved);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe void sqlite3_result_zeroblob(IntPtr context, int n);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe void sqlite3_result_error_toobig(IntPtr context);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe void sqlite3_result_error_nomem(IntPtr context);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe void sqlite3_result_error_code(IntPtr context, int code);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe IntPtr sqlite3_aggregate_context(IntPtr context, int nBytes);

	/*
	[DllImport(SQLITE_DLL, ExactSpelling = true, EntryPoint = "sqlite3_config", CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_config_none(int op);

	[DllImport(SQLITE_DLL, ExactSpelling = true, EntryPoint = "sqlite3_config", CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_config_int(int op, int val);

	[DllImport(SQLITE_DLL, ExactSpelling = true, EntryPoint = "sqlite3_config", CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_config_log(int op, NativeMethods.callback_log func, hook_handle pvUser);


	[DllImport(SQLITE_DLL, ExactSpelling = true, EntryPoint = "sqlite3_db_config", CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_db_config_charptr(sqlite3 db, int op, byte* val);

	[DllImport(SQLITE_DLL, ExactSpelling = true, EntryPoint = "sqlite3_db_config", CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_db_config_int_outint(sqlite3 db, int op, int val, int* result);

	[DllImport(SQLITE_DLL, ExactSpelling = true, EntryPoint = "sqlite3_db_config", CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_db_config_intptr_int_int(sqlite3 db, int op, IntPtr ptr, int int0, int int1);
	*/
	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_create_collation(sqlite3 db, byte[] strName, int nType, hook_handle pvUser, NativeMethods.callback_collation func);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe IntPtr sqlite3_update_hook(sqlite3 db, NativeMethods.callback_update func, hook_handle pvUser);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe IntPtr sqlite3_commit_hook(sqlite3 db, NativeMethods.callback_commit func, hook_handle pvUser);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe IntPtr sqlite3_profile(sqlite3 db, NativeMethods.callback_profile func, hook_handle pvUser);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe void sqlite3_progress_handler(sqlite3 db, int instructions, NativeMethods.callback_progress_handler func, hook_handle pvUser);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe IntPtr sqlite3_trace(sqlite3 db, NativeMethods.callback_trace func, hook_handle pvUser);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe IntPtr sqlite3_rollback_hook(sqlite3 db, NativeMethods.callback_rollback func, hook_handle pvUser);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe IntPtr sqlite3_db_handle(IntPtr stmt);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe IntPtr sqlite3_next_stmt(sqlite3 db, IntPtr stmt);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_stmt_isexplain(sqlite3_stmt stmt);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_stmt_busy(sqlite3_stmt stmt);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_stmt_readonly(sqlite3_stmt stmt);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_exec(sqlite3 db, byte* strSql, NativeMethods.callback_exec cb, hook_handle pvParam, out IntPtr errMsg);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_get_autocommit(sqlite3 db);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_extended_result_codes(sqlite3 db, int onoff);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_errcode(sqlite3 db);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_extended_errcode(sqlite3 db);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe byte* sqlite3_errstr(int rc);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe void sqlite3_log(int iErrCode, byte* zFormat);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_file_control(sqlite3 db, byte[] zDbName, int op, IntPtr pArg);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe sqlite3_backup sqlite3_backup_init(sqlite3 destDb, byte* zDestName, sqlite3 sourceDb, byte* zSourceName);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_backup_step(sqlite3_backup backup, int nPage);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_backup_remaining(sqlite3_backup backup);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_backup_pagecount(sqlite3_backup backup);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_backup_finish(IntPtr backup);

	//[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	//public static extern unsafe int sqlite3_snapshot_get(sqlite3 db, byte* schema, out IntPtr snap);

	//[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	//public static extern unsafe int sqlite3_snapshot_open(sqlite3 db, byte* schema, sqlite3_snapshot snap);

	//[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	//public static extern unsafe int sqlite3_snapshot_recover(sqlite3 db, byte* name);

	//[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	//public static extern unsafe int sqlite3_snapshot_cmp(sqlite3_snapshot p1, sqlite3_snapshot p2);

	//[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	//public static extern unsafe void sqlite3_snapshot_free(IntPtr snap);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_blob_open(sqlite3 db, byte* sdb, byte* table, byte* col, long rowid, int flags, out sqlite3_blob blob);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_blob_write(sqlite3_blob blob, byte* b, int n, int offset);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_blob_read(sqlite3_blob blob, byte* b, int n, int offset);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_blob_bytes(sqlite3_blob blob);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_blob_reopen(sqlite3_blob blob, long rowid);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_blob_close(IntPtr blob);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_wal_autocheckpoint(sqlite3 db, int n);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_wal_checkpoint(sqlite3 db, byte* dbName);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_wal_checkpoint_v2(sqlite3 db, byte* dbName, int eMode, out int logSize, out int framesCheckPointed);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_set_authorizer(sqlite3 db, NativeMethods.callback_authorizer cb, hook_handle pvUser);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_create_function_v2(sqlite3 db, byte[] strName, int nArgs, int nType, hook_handle pvUser, NativeMethods.callback_scalar_function func, NativeMethods.callback_agg_function_step fstep, NativeMethods.callback_agg_function_final ffinal, NativeMethods.callback_destroy fdestroy);

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_keyword_count();

	[DllImport(SQLITE_DLL, ExactSpelling = true, CallingConvention = CALLING_CONVENTION)]
	public static extern unsafe int sqlite3_keyword_name(int i, out byte* name, out int length);

	[UnmanagedFunctionPointer(CALLING_CONVENTION)]
	public delegate void callback_log(IntPtr pUserData, int errorCode, IntPtr pMessage);

	[UnmanagedFunctionPointer(CALLING_CONVENTION)]
	public delegate void callback_scalar_function(IntPtr context, int nArgs, IntPtr argsptr);

	[UnmanagedFunctionPointer(CALLING_CONVENTION)]
	public delegate void callback_agg_function_step(IntPtr context, int nArgs, IntPtr argsptr);

	[UnmanagedFunctionPointer(CALLING_CONVENTION)]
	public delegate void callback_agg_function_final(IntPtr context);

	[UnmanagedFunctionPointer(CALLING_CONVENTION)]
	public delegate void callback_destroy(IntPtr p);

	[UnmanagedFunctionPointer(CALLING_CONVENTION)]
	public delegate int callback_collation(IntPtr puser, int len1, IntPtr pv1, int len2, IntPtr pv2);

	[UnmanagedFunctionPointer(CALLING_CONVENTION)]
	public delegate void callback_update(IntPtr p, int typ, IntPtr db, IntPtr tbl, long rowid);

	[UnmanagedFunctionPointer(CALLING_CONVENTION)]
	public delegate int callback_commit(IntPtr puser);

	[UnmanagedFunctionPointer(CALLING_CONVENTION)]
	public delegate void callback_profile(IntPtr puser, IntPtr statement, long elapsed);

	[UnmanagedFunctionPointer(CALLING_CONVENTION)]
	public delegate int callback_progress_handler(IntPtr puser);

	[UnmanagedFunctionPointer(CALLING_CONVENTION)]
	public delegate int callback_authorizer(IntPtr puser, int action_code, IntPtr param0, IntPtr param1, IntPtr dbName, IntPtr inner_most_trigger_or_view);

	[UnmanagedFunctionPointer(CALLING_CONVENTION)]
	public delegate void callback_trace(IntPtr puser, IntPtr statement);

	[UnmanagedFunctionPointer(CALLING_CONVENTION)]
	public delegate void callback_rollback(IntPtr puser);

	[UnmanagedFunctionPointer(CALLING_CONVENTION)]
	public delegate int callback_exec(IntPtr db, int n, IntPtr values, IntPtr names);
}